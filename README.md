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
    
    
