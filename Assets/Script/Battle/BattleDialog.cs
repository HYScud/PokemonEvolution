using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleDialog : MonoBehaviour
{
    [SerializeField] int wordSpeed=15;
    [SerializeField] TextMeshProUGUI dialogText;

    public void SetDialogText(string paragraph)
    {
        if (dialogText != null)
        {
            dialogText.text = paragraph;
        }
        else
        {
            Debug.LogError("BattleDialog DialogText is null");
        }
    }
    public void AddDialogText(char word)
    {
        if (dialogText != null)
        {
            dialogText.text += word;
        }
        else
        {
            Debug.LogError("BattleDialog DialogText is null");
        }
    }

    public async UniTask<bool> SetDialogByWord(string dialog)
    {
        dialogText.text = string.Empty;
        foreach (var item in dialog.ToCharArray())
        {
            AddDialogText(item);
            await UniTask.WaitForSeconds(1f/wordSpeed);
        }
        await UniTask.WaitForSeconds(0.2f);
        return true;
    }

    public void ClearDialogText()
    {
        if(dialogText != null)
        {
            dialogText.text = string.Empty;
        }
        else
        {
            Debug.LogError("BattleDialog DialogText is null");
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
