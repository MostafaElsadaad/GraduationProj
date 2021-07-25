using System.Collections;
using UnityEngine.Networking;
using UnityEngine;



public class API_Handler : MonoBehaviour
{
    private const string Login_URL = "http://192.168.1.46:3000/validate";
    private const string GameInfo_URL = "http://192.168.1.46:3000/games";
    private float timer, waitingTime = 4.0f;


    public static bool Logged_in = false , Game_Data_Sent = false ;
    private string email = "mostafaali03@hotmail.com", password = "123"  ;
    public static string Patient_ID ; 

    private string[] key = new string[2]; string[] value = new string[2]; 
   // private float timer, waitingTime = 4.0f;

    [System.Serializable]
    public struct patient { public string id; };
    [System.Serializable]
    public struct level { public string id; };
    [System.Serializable]
    public struct game { public string id; public string name; public level[] levels; };
    [System.Serializable]
    public struct game_list { public game[] data; };

    public static game_list Game_Info;
    public static patient Patient; 
   
    private void Start()
    {
        key[0] = "email"; value[0] = email;
        key[1] = "password"; value[1] = password;
        
        
    }
    private void Update()
    {   
        if(Logged_in)
        {
            if (!Game_Data_Sent)
            {

                timer += Time.deltaTime;
                if (timer > waitingTime)
                {
                    Get_Request();
                    timer = 0.0f;
                }
            }
            
        }
        else
        {
            timer += Time.deltaTime;
            if (timer > waitingTime)
            {
                Post_Request(key, value, Login_URL);
                timer = 0.0f;
            }

           
        }
        
        
    }
    public void Post_Request (string [] key , string [] value ,string URL)
    {
        StartCoroutine(Post_Handler(key, value , URL));
    }
    public void Get_Request()
    {
        StartCoroutine(Get_Handler(GameInfo_URL));
    }
    
  
  

  
    IEnumerator Get_Handler(string URL)
    {
        using (UnityWebRequest R = UnityWebRequest.Get(URL))
        {
            // R.SetRequestHeader("hatem", "mohamed"); 
            yield return R.SendWebRequest();

            // string[] pages = URL.Split('/');
            // int page = pages.Length - 1;
            
            if (R.isHttpError)
            {
                Debug.Log(R.error);
            }
            else if (R.isDone)
            {
                
                Debug.Log(R.downloadHandler.text);
                Game_Info = JsonUtility.FromJson<game_list>(R.downloadHandler.text);
                
                
                   
                
             //  Debug.Log(Game_Info.data[0].id + "," + Game_Info.data[0].name + "," + Game_Info.data[0].Levels[0].id + "," + Game_Info.data.Length);
            //   Debug.Log(Game_Info.data[1].id + "," + Game_Info.data[1].name + "," + Game_Info.data[1].Levels[0].id);
                Game_Data_Sent = true;
                

            }
        }
    }
  
    IEnumerator Post_Handler(string [] key , string [] value , string URL)
    {
        WWWForm form = new WWWForm();
        Debug.Log(key[0] + ":" + value[0]);
        Debug.Log(key[1] + ":" + value[1]);
        // string json_text = JsonUtility.ToJson(form);

        for ( int i= 0; i< key.Length; i++ )
        {
            form.AddField(key[i], value[i]);
        }
        using (UnityWebRequest R = UnityWebRequest.Post(URL, form))
        {
           
            yield return R.SendWebRequest();

            if (R.isHttpError)
            {
                Debug.Log(R.error);
            }
            else
            {  
                
                Debug.Log("Form upload complete!");
                Debug.Log(R.downloadHandler.text);
                if(!Logged_in) { Patient = JsonUtility.FromJson<patient>(R.downloadHandler.text); }  
                Logged_in = true; 
                
                // Debug.Log(R.GetResponseHeader("testHeader"));
            }
        }
    }
}
