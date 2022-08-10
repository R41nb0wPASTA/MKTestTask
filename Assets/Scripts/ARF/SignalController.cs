using UnityEngine;
using Zenject;

public class SignalController : IInitializable
{
    readonly SignalBus signalBus;

    public SignalController(SignalBus signalBus)
    {
        this.signalBus = signalBus;
    }

    public void Initialize()
    {
        ARWorldController arWorldController = Transform.FindObjectOfType<ARWorldController>();
        arWorldController.SetSignalBus(signalBus);
        arWorldController.CustomStart();
    }
}
