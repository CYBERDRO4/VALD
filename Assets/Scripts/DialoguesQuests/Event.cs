using UnityEngine;

public class Event : MonoBehaviour
{
    private EventType type;
    public Event(EventType eventType) { type = eventType; }

    public EventType GetEventType() { return type;}

    public void StartEvent() {
        switch (type) {
            case EventType.EnableTransform:
                Nicholas.HumanGhostTransformation.canToTransform = true;
                break;
            case EventType.DisableTransform:
                Nicholas.HumanGhostTransformation.canToTransform = false;
                break;
            case EventType.StopEnemies:
                break;
            }

    }
    public void StopEvent() { }

}
public enum EventType {StopEnemies, EnableTransform, DisableTransform }
