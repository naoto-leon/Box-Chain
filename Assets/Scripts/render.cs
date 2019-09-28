using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class render : Main
{
    LineRenderer _lineRenderer;
    Vector3[] _lerposition;

    public float _generateMultiplier;


    //music
    public float input { get; set; }

    //render propatiy

    public Material _material;
    public Color _color;
    private Material _matInstence;
    public int _audioBandMaterials;
    public float _emissionMultiply;


    /// <summary>

    private float[] _lerpAudio;



    // Start is called before the first frame update
    void Start()
    {
        /// <summary>
        ///
        _lerpAudio = new float[_initiatorPointAmount];

        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.enabled = true;
        _lineRenderer.useWorldSpace = false;
        _lineRenderer.loop = true;
        _lineRenderer.positionCount = _position.Length;
        _lineRenderer.SetPositions(_position);

        _lerposition = new Vector3[_position.Length];

        //apply materials

        _matInstence = new Material(_material);
        _lineRenderer.material = _matInstence;


    }

    // Update is called once per frame
    void Update()
    {
        //apply materials 
        _matInstence.SetColor("_EmissionColor", _color * _audioBandMaterials * _emissionMultiply);
        _matInstence.EnableKeyword("_EMISSION");

        if (_generationCount != 0)
        {

            /// <summary>
            int count = 0;
            for (int i = 0; i < _initiatorPointAmount; i++)
            {
                for (int j = 0; j < (_position.Length) / _initiatorPointAmount; j++)
                {
                    _lerposition[count] = Vector3.Lerp(_position[count], _targetPosition[count], _lerpAudio[i] + (input * .5f));
                    count++;
                }
            }
            _lerposition[count] = Vector3.Lerp(_position[count], _targetPosition[count], _lerpAudio[_initiatorPointAmount - 1] + (input * .5f));


      
            _lineRenderer.positionCount = _lerposition.Length;
            _lineRenderer.SetPositions(_lerposition);

        }

    }
}
