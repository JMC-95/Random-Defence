using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollView : MonoBehaviour
{
    [SerializeField]
    private GameObject _image = null;

    [SerializeField]
    private Transform _content = null;

    void Start()
    {

    }

    public void AddImage()
    {
        var instance = Instantiate(_image);
        instance.transform.SetParent(_content);
    }
}
