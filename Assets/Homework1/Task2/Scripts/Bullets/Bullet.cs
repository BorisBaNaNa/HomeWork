using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private MeshRenderer _mesh;

    Coroutine _destroyCoroutine;
    private LayerMask _collideLayer;
    private Vector3 _lastPos;
    private float _damage;
    private float _speed;
    private bool isActivated;

    public void Construct(StatsBulletConfig statsBullet, float damage)
    {
        _damage = damage;
        _speed = statsBullet.Speed;
        _collideLayer = statsBullet.CollideLayer;
        _lastPos = transform.position;

        //_collider.enabled = true;
        isActivated = true;
        _destroyCoroutine = StartCoroutine(DestroyAfcterTimeRoutine(statsBullet.LifeTime));
    }

    private void OnValidate()
    {
        if (_mesh == null)
            _mesh = GetComponent<MeshRenderer>();
    }

    // странное поведение: содаётся дальше чем надо
    //private void Update()
    //{
    //    if (!isActivated)
    //        return;

    //    MoveToTarget(Time.deltaTime);
    //}

    private void FixedUpdate()
    {
        if (!isActivated)
            return;

        float checkCollideDistance = MoveToTarget(Time.fixedDeltaTime);

        if (Physics.Raycast(_lastPos, transform.forward, out RaycastHit raycastHit, checkCollideDistance, _collideLayer.value))
        {
            OnCollideAction(raycastHit);
        }
        _lastPos = transform.position;
    }

    private float MoveToTarget(float deltaTime)
    {
        Vector3 translation = _speed * deltaTime * Vector3.forward;
        transform.Translate(translation, Space.Self);
        return translation.magnitude;
    }

    private void OnCollideAction(RaycastHit raycastHit)
    {
        Debug.Log($"Bullet is collided with {raycastHit.transform.name}");
        var damagableObj = raycastHit.transform.GetComponentInParent<IDamagable>();
        if (damagableObj != null)
        {
            damagableObj.TakeDamage(_damage);
        }

        DestroyThis();
    }

    private IEnumerator DestroyAfcterTimeRoutine(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    private void DestroyThis()
    {
        _mesh.enabled = false;
        isActivated = false;

        StopCoroutine(_destroyCoroutine);
        Destroy(gameObject, 2);
    }
}