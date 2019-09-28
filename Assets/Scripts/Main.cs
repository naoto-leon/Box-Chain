using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    protected enum _axis
    {
        XAxis,
        YAxis,
        ZAxis
    };

    [SerializeField]
    protected _axis axis = new _axis();


    protected enum _initiator
    {
        Triangle,
        Square,
        Pentagon,
        Hexagon,
        Heptagon,
        Octagon

    };

    //構造体
    public struct LineSegment
    {
        public Vector3 StartPosition { get; set; }
        public Vector3 EndPosition { get; set; }
        public Vector3 Direction { get; set; }
        public float Length { get; set; }
    }




    [SerializeField]
    protected _initiator initiator = new _initiator();

    //animation movement
    [SerializeField]
    protected AnimationCurve _generator;
    protected Keyframe[] _keys;


    //逆内曲
    [System.Serializable]
    // publicをそれぞれinstance化
    public struct StartGen
    {
        public bool outwards;
        public float scale;
    }

    public StartGen[] _startGen;


    protected int _generationCount;

    protected int _initiatorPointAmount;
    private Vector3[] _initiatorPoint;
    private Vector3 _rotateVector;
    private Vector3 _rotateAxis;

    private float _initialrotation;

    [SerializeField]
    protected float _initiatorSize;

    //Awake ins
    protected Vector3[] _position;
    protected Vector3[] _targetPosition;
    //ベジュ曲線
    protected Vector3[] _bezierPosition;

    //構造体リスト
    private List<LineSegment> _lineSegment;


    private void Awake()
    {

        GetInisiatorPoints();
        _position = new Vector3[_initiatorPointAmount + 1];
        _targetPosition = new Vector3[_initiatorPointAmount + 1];
        _lineSegment = new List<LineSegment>();

        _keys = _generator.keys;

        //ラインレンダラー= +1 

        _rotateVector = Quaternion.AngleAxis(_initialrotation, _rotateAxis) * _rotateVector;

        for (int i = 0; i < _initiatorPointAmount; i++)
        {
            _position[i] = _rotateVector * _initiatorSize;
            //z方向かける_initiateSize

            _rotateVector = Quaternion.AngleAxis(360 / _initiatorPointAmount, _rotateAxis) * _rotateVector;

        }
        _position[_initiatorPointAmount] = _position[0];

        _targetPosition = _position;


        //受け渡し

        for (int i = 0; i < _startGen.Length; i++)
        {
            KochGenerator(_targetPosition, _startGen[i].outwards, _startGen[i].scale);
        }
    }

    protected void KochGenerator(Vector3[] position, bool outwards, float generatorMaltiply)
    {
        _lineSegment.Clear();

        for (int i = 0; i < position.Length - 1; i++)
        {
            LineSegment line = new LineSegment();
            //構造体の初期化
            line.StartPosition = position[i];
            if (i == position.Length - 1)
            {
                //最後の番号だったら
                line.EndPosition = position[0];

            }
            else
            {
                line.EndPosition = position[i + 1];
                //startpositionの次　
            }
            //ベクトル　
            line.Direction = (line.EndPosition - line.StartPosition).normalized;
            line.Length = Vector3.Distance(line.EndPosition, line.StartPosition);
            _lineSegment.Add(line);
            //lineに４つの要素
        }

        //ポイントとポイント

        List<Vector3> newpos = new List<Vector3>();
        List<Vector3> targetPos = new List<Vector3>();

        //listの長さ
        for (int i = 0; i < _lineSegment.Count; i++)
        {
            newpos.Add(_lineSegment[i].StartPosition);
            targetPos.Add(_lineSegment[i].StartPosition);


            for (int j = 1; j < _keys.Length - 1; j++)
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
                //bool型　ture  向きの指定　
                {
                    Dir = Quaternion.AngleAxis(-90, _rotateAxis) * _lineSegment[i].Direction;
                    //ベクトル×回転
                    //逆内
                }
                else
                {
                    Dir = Quaternion.AngleAxis(90, _rotateAxis) * _lineSegment[i].Direction;
                }

                newpos.Add(movePos);
                targetPos.Add(movePos + (Dir * heightAmount));

            }

        }

        //初期値

        newpos.Add(_lineSegment[0].StartPosition);
        targetPos.Add(_lineSegment[0].StartPosition);
        _position = new Vector3[newpos.Count];
        _targetPosition = new Vector3[targetPos.Count];

        //配列変換　
        _position = newpos.ToArray();
        _targetPosition = targetPos.ToArray();

        _generationCount++;
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
