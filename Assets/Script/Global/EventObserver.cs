using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public interface EventObserver 
{
    void HandleEvent(EventData resp);
}
