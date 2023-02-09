using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruits : MonoBehaviour
{
    [SerializeField] private GameObject[] _fruitPrefabs;
    [SerializeField] private ParticleSystem _grtaedParticles;
    [SerializeField] private Transform _stopPointTexture;
    [SerializeField] private Transform _invisibleTexture;
    [SerializeField] private InputManager _inputManager;
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _gratingPoint;
    [SerializeField] private float _startAnimationSpeed = 3f;

    private bool _switchigFruit = false;
    private bool _isGrating;
    private Vector3 _upDownPoint;
    private Vector3 _invisibleTexturSatrtPoint;
    private float _gratePercent;
 


    void Start()
    {
        transform.position = _startPoint.position;
        _invisibleTexturSatrtPoint = _invisibleTexture.localPosition;
        
        SelectRandomFruitPrefab();
    }

    private void FixedUpdate()
    {
        GrateTheFruit();
        

    }

    private void GrateTheFruit()
    {
        if (_inputManager.GetUserInput() && !_switchigFruit)
        {
            if(_isGrating)
            {
                
                if (transform.position.y >= 0.7f)
                {
                    _upDownPoint = new Vector3(transform.position.x,- 0.3f, transform.position.z);
                    _startAnimationSpeed = 12f;
                }
                else if(transform.position.y <= -0.2f)
                {
                    _upDownPoint = new Vector3(transform.position.x, 0.75f, transform.position.z);
                    _startAnimationSpeed = 15f;
                }

                transform.position = Vector3.Lerp(transform.position, _upDownPoint, Time.fixedDeltaTime * _startAnimationSpeed);
                _invisibleTexture.position = Vector3.Lerp(_invisibleTexture.position, _stopPointTexture.position, Time.fixedDeltaTime);
                GraterPercent();
                
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, _gratingPoint.position, Time.fixedDeltaTime * _startAnimationSpeed);
            }
        }
        else
        {
            _startAnimationSpeed = 3f;
            _isGrating = false;
            transform.position = Vector3.Lerp(transform.position, _startPoint.position, Time.fixedDeltaTime * _startAnimationSpeed);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Grate"))
        {
            _isGrating = true;
            _grtaedParticles.Play();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Grate"))
        {
            
            _grtaedParticles.Stop();
        }
    }

    private void SelectRandomFruitPrefab()
    {
        ResetInvisibleTexturePosition();
        foreach (GameObject fruit in _fruitPrefabs)
        {
            fruit.SetActive(false);
        }
        int randomFruitIndex = Random.Range(0, _fruitPrefabs.Length);
        _fruitPrefabs[randomFruitIndex].SetActive(true);  
    }

    private void ResetInvisibleTexturePosition()
    {
        _invisibleTexture.localPosition = _invisibleTexturSatrtPoint;
    }

    private void GraterPercent()
    {
        _gratePercent =  Vector3.Distance(_invisibleTexture.position,_stopPointTexture.position);
        if (_gratePercent < 0.02)
            StartCoroutine(SwitchFruit());
            
    }

    private IEnumerator SwitchFruit()
    {
        _switchigFruit = true;
        yield return new WaitForSecondsRealtime(1f);
        SelectRandomFruitPrefab();
        yield return new WaitForSecondsRealtime(1f);
        _switchigFruit = false;
    }
}
