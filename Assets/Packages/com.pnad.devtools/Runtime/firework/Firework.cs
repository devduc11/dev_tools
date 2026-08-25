using System.Collections.Generic;
using Eagle.BaseGame;
using Teo.AutoReference;
using UnityEngine;

public class Firework : BaseMonoBehaviour
{
    [SerializeField, GetInChildren] private List<ParticleSystem> _fireworks;

    protected override void Start()
    {
        base.Start();
        StopFx();
    }

    public void StopFx()
    {
        foreach (var item in _fireworks)
        {
            item.Stop();
        }
    }

    public void PlayFx()
    {
        foreach (var item in _fireworks)
        {
            item.Play();
        }
        // SoundManager.Instance.PlaySFX(SoundType.Claim);
    }
}