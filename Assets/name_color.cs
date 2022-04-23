using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class name_color : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] TextMeshProUGUI textobj;
    void Start()
    {
     //   textobj = this.GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        if (textobj.text.Equals("Rita")) { textobj.color = new Color(0.89f, 0.84f, 0.44f); }
    
        if (textobj.text.Equals("Amo")) { textobj.color = new Color(0.94f, 0.97f, 1.00f); }
        if (textobj.text.Equals("Mr. Rabbit")) { textobj.color = new Color(0.49f, 0.73f, 0.91f); }

    }
}
