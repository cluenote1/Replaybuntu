using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireController : MonoBehaviour
{
    [SerializeField] private Transform playerHand;
    [SerializeField] private Vector2 shotPos; // Vector3�� Vector2�� ����
    [SerializeField] private float shotSpeed = 10f;
    [SerializeField] private float maxDistance = 9f;
    [SerializeField] private AudioClip shotSound;
    [SerializeField] private LayerMask wireBuilding;

    private float wireDistance;
    public float WireDistance => wireDistance;

    private bool onWire;
    public bool OnWire => onWire;

    private LineRenderer line;
    private Transform DRB;
    private RaycastHit2D hit; // RaycastHit�� RaycastHit2D�� ����
    private bool isWire;

    private void Awake()
    {
        DRB = transform.GetChild(0);
        line = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        DisableWire();
    }

    private void Update()
    {
        line.SetPosition(0, playerHand.position);
        line.SetPosition(1, transform.position);
    }

    public void ShotWire()
    {
        if (isWire) return;
        SoundManager.Instance.SFXPlay("ShotWire", shotSound);
        isWire = true;
        DRB.gameObject.SetActive(true);
        line.enabled = true;
        StartCoroutine(Shot());
    }

    public void DisableWire()
    {
        onWire = false;
        isWire = false;
        DRB.gameObject.SetActive(false);
        line.enabled = false;
        transform.parent = null;
    }

    private IEnumerator Shot()
    {
        Vector2 lastHandPos = playerHand.position; // Vector3�� Vector2�� ����
        transform.position = lastHandPos;
        Vector2 targetPos = (shotPos - lastHandPos).normalized * maxDistance;
        wireDistance = Vector2.Distance(transform.position, targetPos + lastHandPos);

        while (wireDistance > 0f)
        {
            wireDistance = Vector2.Distance(transform.position, targetPos + lastHandPos);
            transform.position = Vector2.MoveTowards(transform.position, targetPos + lastHandPos, Time.deltaTime * shotSpeed);
            Debug.DrawRay(lastHandPos, (targetPos - (targetPos.normalized * (maxDistance - wireDistance))), Color.white, 0.2f);
            RaycastHit2D hit = Physics2D.Raycast(lastHandPos, (targetPos - lastHandPos).normalized, maxDistance - wireDistance, wireBuilding);

            if (hit.collider != null)
            {
                Target(hit);
                break;
            }
            yield return null;
        }
        if (wireDistance <= 0f)
            DisableWire();
    }

    private void Target(RaycastHit2D hit)
    {
        transform.parent = hit.transform; // ������ �θ� ����
        onWire = true;
    }
}