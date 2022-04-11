using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blocker_manager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<GameObject> blocks;
    public  List<GameObject> indicator;
    float intial_y_pos=0;
    float halfHeight;
    float distance;
    int closest_index;
    int current_index;
    bool close;
    int storage=2;
    public float max_height;
    GameObject last_object;
    bool overload;
    bool remove;
    GameObject current_indicator;
    [SerializeField] GameObject storage_indicator;
    [SerializeField] GameObject UI_warning;
    [SerializeField] GameObject UI_Exit;
    public  static GameObject drag_object;
    
    void Start()
    {
        sortblocker();
        for (int i = 0; i < 8; i++)
        {
            current_indicator = Instantiate(storage_indicator, new Vector3(transform.position.x - 1.7f, transform.position.y+0.2f + i * 0.3f, 0), Quaternion.Euler(0, 0, 90));
                
            indicator.Add(current_indicator);

        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < indicator.Count; i++)
        {
            if (i < storage) { indicator[i].SetActive(true); }
            else { indicator[i].SetActive(false); }
            if (storage > 5) { indicator[i].GetComponent<SpriteRenderer>().color = Color.red;UI_warning.SetActive(true);UI_Exit.SetActive(false); } else { indicator[i].GetComponent<SpriteRenderer>().color = Color.gray; UI_warning.SetActive(false); UI_Exit.SetActive(true); }

        }


        if ((blocks[blocks.Count - 1].transform.position.y - blocks[0].transform.position.y) > max_height) { overload = true; } else { overload = false; }
        
        if (drag_object != null) {
            if (blocks.Contains(drag_object))
            {
                if (Vector2.Distance(drag_object.transform.position, this.transform.position) > 3)
                {
                    remove = true;
                  
                    last_object = drag_object;
                   
                }
                else { remove = false;}
               
                
            }
            else
            {
                if (Vector2.Distance(drag_object.transform.position, this.transform.position) < 3)
                {
                    search();
                    close = true;
                    last_object = drag_object;
                }
                else { close = false; sortblocker(); }

            }
        }

        if(drag_object==null) {

            if (remove)
            {
                blocks.Remove(last_object);
                last_object.GetComponent<drag_neural>().back();
                remove = false;
                storage = storage - 3;
            }
            else
            {
                Debug.Log(remove);
                sortblocker();
            }

            if (close)
            {  
                addblock(last_object);
                close = false;
                storage = storage + 3;
            }
            else { last_object.GetComponent<drag_neural>().back(); }

            
            Debug.Log("run");
            last_object = null;
            sortblocker();
        }

    }

    void sortblocker()
    {
        intial_y_pos = transform.position.y;
        foreach (GameObject i in blocks)
        {
            halfHeight = i.GetComponent<SpriteRenderer>().bounds.size.y / 2;
            i.transform.position = new Vector3(transform.position.x, intial_y_pos + halfHeight, 0);
            intial_y_pos = intial_y_pos + halfHeight * 2;
        }
        
    }
    void search()
    {
        distance = 1000;
        for (int i= 0;i<blocks.Count; i++)
        {
            float distance_a = Vector2.Distance(drag_object.transform.position, blocks[i].transform.position);
            if (distance>distance_a) {
                distance = distance_a;
                closest_index = i;
                
            }
        }

        if (current_index!=closest_index) { gap(); current_index = closest_index; }
       
 
        
    }
    void addblock(GameObject block)
    {

        blocks.Insert(closest_index,block);
        sortblocker();
    }
    void gap()
    {
        sortblocker();
        halfHeight = drag_object.GetComponent<SpriteRenderer>().bounds.size.y / 2;
        for (int i = closest_index; i < blocks.Count; i++)
        {
            blocks[i].transform.position = new Vector3(this.transform.position.x, blocks[i].transform.position.y + 2*halfHeight, 0);
        }
        
      
       
    }
}
