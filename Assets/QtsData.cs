using UnityEngine;

[CreateAssetMenu(fileName = "New QuestionData", menuName = "QuestionData")]  // Corrected 'filename' to 'fileName'
public class QtsData : ScriptableObject
{
    [System.Serializable]
    public struct QuestionData
    {
        public string questionText;
        public string[] replies;
        public int correctReplyIndex;
    }

    public QuestionData[] questions;
}
