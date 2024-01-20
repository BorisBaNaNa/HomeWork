using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private MeshRenderer _mesh;

    Coroutine _destroyCoroutine;
    private LayerMask _collideLayer;
    private float _damage;
    private float _speed;
    private bool isActivated;

    public void Construct(StatsBulletConfig statsBullet, float damage)
    {
        _damage = damage;
        _speed = statsBullet.Speed;
        _collideLayer = statsBullet.CollideLayer;

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

    //    Move(Time.deltaTime);
    //}

    private void FixedUpdate()
    {
        if (!isActivated)
            return;

        Vector3 translation = _speed * Time.deltaTime * Vector3.forward;
        float checkCollideDistance = translation.magnitude;

        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit raycastHit, checkCollideDistance, _collideLayer.value))
        {
            transform.position = raycastHit.point;
            OnCollideAction(raycastHit);
        }
        else Move(translation);
    }

    private void Move(Vector3 translation)
    {
        transform.Translate(translation, Space.Self);
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