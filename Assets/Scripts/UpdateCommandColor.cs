using UnityEngine;
using UnityEngine.UI;

public class UpdateCommandColor : MonoBehaviour
{
    [SerializeField]
    private string m_messageToListenFor;

    [SerializeField]
    private Graphic m_target;

    [SerializeField]
    private Color m_colorToSet;
}