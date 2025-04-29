using TMPro;
using UnityEngine;

namespace _Project.Code.Gameplay.Subjects
{
    public class SubjectViev : MonoBehaviour
    {
        [SerializeField] private Subject _subject;
        [SerializeField] private TMP_Text _vievSubject;

        public void SetDisplay()
        {
            string displayValue = _subject.SubjectType switch
            {
                TypeSubject.Triangle => "1",
                TypeSubject.Circle => "2",
                TypeSubject.Square => "3",
                _ => "?"
            };

            _vievSubject.text = displayValue;
        }
    }
}
