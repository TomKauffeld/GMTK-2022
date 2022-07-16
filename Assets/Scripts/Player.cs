using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 1f;
    public Settings settings;
    public LevelManager levelManager;

    private Vector3 Movement = Vector3.zero;

    private void Start()
    {
        EventSystem.OnLevelChange += LevelChanged;
    }

    private void OnDestroy()
    {
        EventSystem.OnLevelChange -= LevelChanged;
    }

    private void LevelChanged(object sender, Level e)
    {
        transform.position = new Vector3(0, 0, -20);
    }

    // Update is called once per frame
    void Update()
    {
        Movement = Vector3.zero;
        if (GameLogic.Active)
        {
            if (Input.GetMouseButtonDown(0))
                Click();

            if (Input.GetKey(settings.Up))
                Movement.y += speed;
            if (Input.GetKey(settings.Down))
                Movement.y -= speed;
            if (Input.GetKey(settings.Left))
                Movement.x -= speed;
            if (Input.GetKey(settings.Right))
                Movement.x += speed;
        }
        transform.position += Movement * Time.unscaledDeltaTime;
        ResetPosition();
    }


    private void ResetPosition()
    {
        if (!levelManager.CurrentLevel)
            return;
        if (levelManager.CurrentLevel.TryGetComponent(out BoxCollider collider))
        {
            Vector3 bottomRight = collider.transform.position + collider.center - collider.size;
            Vector3 topLeft = collider.transform.position + collider.center + collider.size;

            Vector3 pos = transform.position;

            if (pos.x < bottomRight.x)
                pos.x = bottomRight.x;

            if (pos.x > topLeft.x)
                pos.x = topLeft.x;

            if (pos.y < bottomRight.y)
                pos.y = bottomRight.y;

            if (pos.y > topLeft.y)
                pos.y = topLeft.y;

            transform.position = pos;
        }
    }



    private void Click()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit) && hit.transform.gameObject.TryGetComponent(out IClickable clickable))
        {
            if (GameLogic.Instance)
                GameLogic.Instance.SoundClick();
            clickable.Click();
        }

    }
}
