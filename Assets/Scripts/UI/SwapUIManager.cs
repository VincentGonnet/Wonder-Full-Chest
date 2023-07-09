using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SwapUIManager : MonoBehaviour
{
    private float slideVelocity;
    private float fillingAmount;
    private Quaternion rotateAmount = new Quaternion(0f, 0f, 0f, 1f);

    public void StartAnimation()
    {
        StartCoroutine(waiter());
    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Image>().fillAmount = Mathf.SmoothDamp(this.GetComponent<Image>().fillAmount, fillingAmount, ref slideVelocity, 0.2f);
        this.transform.Find("Image").rotation = Quaternion.RotateTowards(this.transform.Find("Image").rotation, rotateAmount, 120f * Time.deltaTime);
    }

    IEnumerator waiter()
    {
        fillingAmount = 1f;
        rotateAmount = new Quaternion(0, 0, 180f, 1f);
        yield return new WaitForSeconds(0.3f);
        this.transform.Find("Text").GetComponent<TextMeshProUGUI>().enabled = true;
        this.transform.Find("Image").GetComponent<Image>().enabled = true;
        rotateAmount = new Quaternion(0, 0, 0, 1f);
        yield return new WaitForSeconds(1.2f);
        this.GetComponent<Image>().fillOrigin = (int) Image.OriginHorizontal.Right;
        fillingAmount = 0f;
        yield return new WaitForSeconds(0.2f);
        this.transform.Find("Text").GetComponent<TextMeshProUGUI>().enabled = false;
        this.transform.Find("Image").GetComponent<Image>().enabled = false;
        rotateAmount = new Quaternion(0, 0, 180f, 1f);
    }
}
