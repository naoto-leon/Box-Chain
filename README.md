# Box-Chain

#### [Box-Chain(youtube)](https://youtu.be/mboTPyyZJmc)
![Boc_Chain](https://user-images.githubusercontent.com/43961147/65813586-4d8ebe80-e212-11e9-953b-141c729cb2ab.gif)

#### [参考(Peer Play)](https://www.patreon.com/peerplay)  
#### [使用(keijiro/Lasp)](https://github.com/keijiro/Lasp/blob/master/README.md)  

#### アクセス修飾子  

public - どこからでも使える  
private - そのクラスしか使えない  
protected - 子クラスからはつかえる。継承する時つかう  
virtual - オーバーライドする元の関数につける  
abstract - 実体はない、インターフェイス的にベースクラスに定義するものっぽい  

#### List_enum  

##### (Exam) 
    public class enumtest : MonoBehaviour
      {
          protected enum fru
       {
           apple,
           orange,
           banana
        };

    [SerializeField]
    protected fru _fru = new fru();

    // Update is called once per frame
    void update()
    {
        switch (_fru)
        {
            case fru.apple:
                Debug.Log("apple");
                break;
            case fru.orange:
                Debug.Log("Orange");
                break;
            case fru.banana:
                Debug.Log("banana");
                break;
            default:
                Debug.Log("Non");
                break;
        }
    }
      }

#### Struct 構造体　プロパティ　

##### (Exam) 

    public class Stracttest : MonoBehaviour
    {
    
    public struct BoxMoveDevelop
    {
        public Vector3 StartPosition { get; set; }
        public Vector3 EndPosition { get; set; }
        public Vector3 Direction { get; set; }       
    }

    public GameObject[] chposition;
    
    // Start is called before the first frame update
    void Start()
    {
        chposition = new GameObject[transform.childCount];
        chposition[0] = transform.GetChild(0).gameObject;
        chposition[1] = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        BoxMoveDevelop BMD = new BoxMoveDevelop();

        BMD.StartPosition = chposition[0].transform.position;
        BMD.EndPosition = chposition[1].transform.position;
        BMD.Direction = (BMD.StartPosition - BMD.EndPosition).normalized;

        Debug.Log(BMD.StartPosition);
        Debug.Log(BMD.EndPosition);
        Debug.Log(BMD.Direction);

     }
    }
    
  *** 
  
  #### [レンダラーの描画]  
 
 #### RenderLine  
 
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;


    [RequireComponent(typeof(LineRenderer))]

    public class RenderLine : BoxChain
    {

        LineRenderer _lineRenderer;
        Vector3[] _lerpPosition;


        public Material _material;
        public Color _color;
        private Material _matInstence;
        //public int _audioBandMaterials;
        public float _emissionMultiply;


        private float[] _lerpAudio;

        // Start is called before the first frame update
        void Start()
        {
            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.enabled = true;
            _lineRenderer.useWorldSpace = false;
            _lineRenderer.loop = true;
            _lineRenderer.positionCount = _position.Length;
            _lineRenderer.SetPositions(_position);

            _lerpPosition = new Vector3[_position.Length];
            //vec3配列

            _matInstence = new Material(_material);
            _lineRenderer.material = _matInstence;
        }

        // Update is called once per frame
        void Update()
        {
            _matInstence.SetColor("_EmissionColor", _color * _emissionMultiply);
            _matInstence.EnableKeyword("_EMISSION");


            //レンダー描画

            if (_generationCount !=0)
            {
                int count = 0;

                for (int i = 0; i < _initiatorPointAmount; i++)
                {
                    //_initiatorPointAmount分のfor
                    for (int j = 0; j < (_position.Length/_initiatorPointAmount); j++)
                    {
                        _lerpPosition[count] = Vector3.Lerp(_position[count], _targetPosition[count],_lerpAudio[i]);
                        count++;
                        //j分count++
                    }
                }
        _lerpPosition[count] = Vector3.Lerp(_position[count], _targetPosition[count], _lerpAudio[_initiatorPointAmount - 1] );
                //謎　

                _lineRenderer.positionCount = _lerpPosition.Length;
                _lineRenderer.SetPositions(_lerpPosition);

            }

        }
    }

#### BoxChain 

    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class BoxChain : MonoBehaviour
    {
        protected enum _axis
        {
            XAxis,
            YAxis,
            ZAxis

        };

        protected enum _initiator
        {
            Triangle,
            Square,
            Pentagon,
            Hexagon,
            Heptagon,
            Octagon

        };

        [SerializeField]
        protected _initiator initiator = new _initiator();
        [SerializeField]
        protected _axis axis = new _axis();

        protected int _initiatorPointAmount;
        private Vector3[] _initiatorPoint;
        private Vector3 _rotateVector;
        private Vector3 _rotateAxis;

        private float _initialrotation;

        [SerializeField]
        protected float _initiatorSize;


        //to render 
        protected Vector3[] _position;
        protected Vector3[] _targetPosition;
        protected int _generationCount;


        [System.Serializable]
        public struct StartGen
        {
            public bool outwards;
            public float scale;
        }

        public StartGen[] _startGen;

        private void Awake()
        {
            GetInisiatorPoints();

            _position = new Vector3[_initiatorPointAmount + 1];
            _targetPosition = new Vector3[_initiatorPointAmount + 1];
            //三角なら4点で出来てる

            _rotateVector = Quaternion.AngleAxis(_initialrotation, _rotateAxis) * _rotateVector;

            for (int i = 0; i < _initiatorPointAmount; i++)
            {
                _position[i] = _rotateVector * _initiatorSize;
                //z方向かける_initiateSize

                _rotateVector = Quaternion.AngleAxis(360 / _initiatorPointAmount, _rotateAxis) * _rotateVector;

            }

            _position[_initiatorPointAmount] = _position[0] ;
            //初期値の0を最後に繋げる　三角なら　配列の0を3に繋げてる
            //初期値の値_position[0]の値
            //通常は0,0,0になってしまう

            _targetPosition = _position;
            //_targetpositionの移動　


        }

        private void GetInisiatorPoints()
        {
            switch (initiator)
            {
                case _initiator.Triangle:
                    _initiatorPointAmount = 3;
                    _initialrotation = 0;
                    break;

                case _initiator.Square:
                    _initiatorPointAmount = 4;
                    _initialrotation = 45;
                    break;

                case _initiator.Pentagon:
                    _initiatorPointAmount = 5;
                    _initialrotation = 36;
                    break;

                case _initiator.Hexagon:
                    _initiatorPointAmount = 6;
                    _initialrotation = 30;
                    break;

                case _initiator.Heptagon:
                    _initiatorPointAmount = 7;
                    _initialrotation = 25.71428f;
                    break;

                case _initiator.Octagon:
                    _initiatorPointAmount = 8;
                    _initialrotation = 22.5f;
                    break;

                default:
                    _initiatorPointAmount = 3;
                    _initialrotation = 0;
                    break;

            }

            switch (axis)
            {
                case _axis.XAxis:
                    _rotateVector = new Vector3(1, 0, 0);
                    _rotateAxis = new Vector3(0, 0, 1);
                    break;

                case _axis.YAxis:
                    _rotateVector = new Vector3(0, 1, 0);
                    _rotateAxis = new Vector3(1, 0, 0);
                    break;

                case _axis.ZAxis:
                    _rotateVector = new Vector3(0, 0, 1);
                    _rotateAxis = new Vector3(0, 1, 0);
                    break;

                default:
                    _rotateVector = new Vector3(0, 1, 0);
                    _rotateAxis = new Vector3(1, 0, 0);
                    break;

            }

        }
    }

  #### [animationCurve keyframとの連携]   
  
   #### RenderLine   
   
       using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;


    [RequireComponent(typeof(LineRenderer))]

    public class RenderLine : BoxChain
    {

        LineRenderer _lineRenderer;
        Vector3[] _lerpPosition;


        public Material _material;
        public Color _color;
        private Material _matInstence;
        //public int _audioBandMaterials;
        public float _emissionMultiply;


        private float[] _lerpAudio;

        // Start is called before the first frame update
        void Start()
        {
            _lerpAudio = new float[_initiatorPointAmount];

            _lineRenderer = GetComponent<LineRenderer>();
            _lineRenderer.enabled = true;
            _lineRenderer.useWorldSpace = false;
            _lineRenderer.loop = true;
            _lineRenderer.positionCount = _position.Length;
            _lineRenderer.SetPositions(_position);

            _lerpPosition = new Vector3[_position.Length];
            //vec3配列

            _matInstence = new Material(_material);
            _lineRenderer.material = _matInstence;
        }

        // Update is called once per frame
        void Update()
        {
            _matInstence.SetColor("_EmissionColor", _color * _emissionMultiply);
            _matInstence.EnableKeyword("_EMISSION");


            //レンダー描画

            if (_generationCount !=0)
            {
                int count = 0;

                for (int i = 0; i < _initiatorPointAmount; i++)
                {
                    //_initiatorPointAmount分のfor
                    for (int j = 0; j < (_position.Length / _initiatorPointAmount); j++)
                    {
                        _lerpPosition[count] = Vector3.Lerp(_position[count], _targetPosition[count],_lerpAudio[i]*.5f);
                        count++;
                        //j分count++
                    }
                }
       _lerpPosition[count] = Vector3.Lerp(_position[count], _targetPosition[count], _lerpAudio[_initiatorPointAmount - 1] );
                

                _lineRenderer.positionCount = _lerpPosition.Length;
                _lineRenderer.SetPositions(_lerpPosition);

            }

        }
    }  
    
 #### BoxChain   
 
     using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class BoxChain : MonoBehaviour
    {
        protected enum _axis
        {
            XAxis,
            YAxis,
            ZAxis

        };

        protected enum _initiator
        {
            Triangle,
            Square,
            Pentagon,
            Hexagon,
            Heptagon,
            Octagon

        };


        [SerializeField]
        protected _axis axis = new _axis();

        [SerializeField]
        protected _initiator initiator = new _initiator();



        protected int _initiatorPointAmount;
        private Vector3[] _initiatorPoint;
        private Vector3 _rotateVector;
        private Vector3 _rotateAxis;

        private float _initialrotation;

        [SerializeField]
        protected float _initiatorSize;


        //to render 
        protected Vector3[] _position;
        protected Vector3[] _targetPosition;
        protected int _generationCount;


        [System.Serializable]
        public struct StartGen
        {
            public bool outwards;
            public float scale;
        }

        public struct LineSegment
        {
            public Vector3 StartPosition { get; set; }
            public Vector3 EndPosition { get; set; }
            public Vector3 Direction { get; set; }
            public float Length { get; set; }

        }


        public StartGen[] _startGen;

        private List<LineSegment> _lineSegment;


        [SerializeField]
        protected AnimationCurve _generator;
        protected Keyframe[] _keys;



        private void Awake()
        {

            GetInisiatorPoints();

            _keys = _generator.keys;
            _lineSegment = new List<LineSegment>();

            _position = new Vector3[_initiatorPointAmount + 1];
            _targetPosition = new Vector3[_initiatorPointAmount + 1];
            //三角なら4点で出来てる



            _rotateVector = Quaternion.AngleAxis(_initialrotation, _rotateAxis) * _rotateVector;

            for (int i = 0; i < _initiatorPointAmount; i++)
            {
                _position[i] = _rotateVector * _initiatorSize;
                //z方向かける_initiateSize

                _rotateVector = Quaternion.AngleAxis(360 / _initiatorPointAmount, _rotateAxis) * _rotateVector;

            }

            _position[_initiatorPointAmount] = _position[0] ;
            //初期値の0を最後に繋げる　三角なら　配列の0を3に繋げてる
            //初期値の値_position[0]の値
            //通常は0,0,0になってしまう

            _targetPosition = _position;
            //_targetpositionの移動　


            for (int i = 0; i < _startGen.Length; i++)
            {
                KochGenerator(_targetPosition, _startGen[i].outwards, _startGen[i].scale);
                              //(Vector3[] position, bool outwards, float generatorMaltiply)
            }

        }


        protected void KochGenerator(Vector3[] position, bool outwards, float generatorMaltiply)
        {
            _lineSegment.Clear();

            for (int i = 0; i < position.Length -1 ; i++)
            {
                LineSegment line = new LineSegment();
                line.StartPosition = position[i];
                //頂点座標がstartposition

                if (i == position.Length -1)
                {
                    line.EndPosition = position[0];
                    //最後なら塞ぐのでendpositionは[0]
                }
                else
                {
                    line.EndPosition = position[i + 1];
                    //それ以外はendpositionは頂点の次(startpositionの次)
                }

                line.Direction = (line.EndPosition - line.StartPosition).normalized;
                line.Length = Vector3.Distance(line.EndPosition, line.StartPosition);
                _lineSegment.Add(line);

            }

            //ポイントとポイント

            List<Vector3> newpos = new List<Vector3>();
            List<Vector3> targetPos = new List<Vector3>();


            for (int i = 0; i < _lineSegment.Count; i++)
            {
                newpos.Add(_lineSegment[i].StartPosition);
                targetPos.Add(_lineSegment[i].StartPosition);

                //keyフレーム

                for (int j = 1; j < _keys.Length -1 ; j++)
                {
                    //keyframeの点は５つなら使うのは3つ(真ん中)

                    float moveAmount = _lineSegment[i].Length * _keys[j].time;
                    //length = 二点間のdistance time = keyframeの横軸

                    float heightAmount = (_lineSegment[i].Length * _keys[j].value) * generatorMaltiply;
                    //generatorMaltiply = 調整

                    Vector3 movePos = _lineSegment[i].StartPosition + (_lineSegment[i].Direction * moveAmount);
                    //スタート位置 + (ベクトル×横軸)


                    Vector3 Dir;

                    if (outwards)
                    {
                        Dir = Quaternion.AngleAxis(-90, _rotateAxis) * _lineSegment[i].Direction;
                    }
                    else
                    {
                        Dir = Quaternion.AngleAxis(90, _rotateAxis) * _lineSegment[i].Direction;
                    }

                    newpos.Add(movePos);
                    targetPos.Add(movePos + (Dir * heightAmount));
                    //movepos=横軸　　
                }
            }

            newpos.Add(_lineSegment[0].StartPosition);
            targetPos.Add(_lineSegment[0].StartPosition);

            _position = new Vector3[newpos.Count];
            //_lineRenderer.PositionsCountの更新
            _targetPosition = new Vector3[targetPos.Count];
            //_lineRenderer.SetPositionsの更新


            _position = newpos.ToArray();
            //_positionにnewpos適用　newposはlistなのでarray変換　
            _targetPosition = targetPos.ToArray();

            _generationCount++;
            //renderline起動ジェネレーター　
        }



        private void GetInisiatorPoints()
        {
            switch (initiator)
            {
                case _initiator.Triangle:
                    _initiatorPointAmount = 3;
                    _initialrotation = 0;
                    break;

                case _initiator.Square:
                    _initiatorPointAmount = 4;
                    _initialrotation = 45;
                    break;

                case _initiator.Pentagon:
                    _initiatorPointAmount = 5;
                    _initialrotation = 36;
                    break;

                case _initiator.Hexagon:
                    _initiatorPointAmount = 6;
                    _initialrotation = 30;
                    break;

                case _initiator.Heptagon:
                    _initiatorPointAmount = 7;
                    _initialrotation = 25.71428f;
                    break;

                case _initiator.Octagon:
                    _initiatorPointAmount = 8;
                    _initialrotation = 22.5f;
                    break;

                default:
                    _initiatorPointAmount = 3;
                    _initialrotation = 0;
                    break;

            }

            switch (axis)
            {
                case _axis.XAxis:
                    _rotateVector = new Vector3(1, 0, 0);
                    _rotateAxis = new Vector3(0, 0, 1);
                    break;

                case _axis.YAxis:
                    _rotateVector = new Vector3(0, 1, 0);
                    _rotateAxis = new Vector3(1, 0, 0);
                    break;

                case _axis.ZAxis:
                    _rotateVector = new Vector3(0, 0, 1);
                    _rotateAxis = new Vector3(0, 1, 0);
                    break;

                default:
                    _rotateVector = new Vector3(0, 1, 0);
                    _rotateAxis = new Vector3(1, 0, 0);
                    break;

            }

        }
    }

    
