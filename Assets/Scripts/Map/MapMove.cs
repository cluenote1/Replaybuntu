// using ������ �׻� Ŭ���� ���� ���� ��ġ�ؾ� �մϴ�.
using UnityEngine;

public class MapMove : MonoBehaviour
{
    public float speed = 2f;
    public float resetPositionX = -10f;
    public float startPositionX = 10f;

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        if (transform.position.x < resetPositionX)
        {
            ResetMap();
        }
    }

    void ResetMap()
    {
        Vector2 newPosition = new Vector2(startPositionX, transform.position.y);
        transform.position = newPosition;
    }
}
