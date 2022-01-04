using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    [SerializeField]
    private float joystickMaxDistanceFromCenter = 50f;

    private RawImage joystickImage;
    private RawImage joystickBackgroundImage;

    private Vector3 movement;
    public Vector3 Movement {get { return movement; } }

    void Start()
    {
        RawImage[] images = GetComponentsInChildren<RawImage>();
        joystickBackgroundImage = images[0];
        joystickImage = images[1];
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 direction;
        //trasforma la posizione del dito sullo schermo in una posizione locale a joystickBackgroundImage.rectTransform
        //nota: ho cambiato il pivot di joystickBackgroundImage.rectTransform per centrarlo
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickBackgroundImage.rectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out direction// out è come passare per riferimento, viene salvato qui la posizione ritornata dalla funzione
            ))
        {
            //entra se rimango col dito sul piano dove si trova joystickBackgroundImage.rectTransform, quindi sempre nel nostro caso

            //le due linee successive mi servono per misurare quanto il punto in cui mi trovo sia distante dal centro
            //moltiplico per 1.4 per avere la sensibilità perfetta, la velocità sarà minima quando tocco al centro e sarà massima
            //quando sarò al lato finale del quadrato del background del josytick
            direction.x = (direction.x / joystickBackgroundImage.rectTransform.sizeDelta.x) * 1.4f;
            direction.y = (direction.y / joystickBackgroundImage.rectTransform.sizeDelta.y) * 1.4f;

            //ottimizzazione possibile??? direction.magnitude > 1.4, la sqrt è costosa
            direction = (direction.sqrMagnitude >1)? direction.normalized : direction;
            movement = new Vector3(direction.x, 0f, direction.y);

            joystickImage.rectTransform.anchoredPosition = new Vector2(direction.x * joystickMaxDistanceFromCenter,
                direction.y * joystickMaxDistanceFromCenter);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        movement = default(Vector3);
        joystickImage.rectTransform.anchoredPosition = default(Vector3);
    }

}
