using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloseButton : MonoBehaviour
{
    public AudioClip closeClip;

    void Start()
    {
        transform.GetComponent<Button>().onClick.AddListener(_Close);
    }

    //close in game
    public void _Close()
    {
        //AudioManager.Instance.soundSource.PlayOneShot(closeClip);
        //transform.parent.GetComponent<RectTransform>().DOScale(Vector3.zero, .25f).SetEase(Ease.InOutQuad).SetUpdate(true) //gameObject: form
        //    .OnComplete(() =>
        //    {
        //        transform.parent.parent.gameObject.SetActive(false); //panel
        //        transform.parent.localScale = Vector3.one;
        //    });

        AudioManager.instance.soundSource.PlayOneShot(closeClip);
        transform.parent.gameObject.SetActive(false);

    }
}
