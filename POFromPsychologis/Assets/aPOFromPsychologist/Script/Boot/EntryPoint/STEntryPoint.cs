using DG.Tweening;
using UnityEngine;

namespace DiplomGames
{
    public class STEntryPoint : EntryPoint<STEntryPoint>
    {        
        [Header("Scene Dependencies")]
        [SerializeField] private STColorValidator colorValidator;
        [SerializeField] private STGameController gameController;
        [SerializeField] private STBuilderGame builderGame;
        [SerializeField] private STHistoryColor historyColor;
        [SerializeField] private STSimonWheel simonWheel;
        [SerializeField] private STUiView uiView;
        [SerializeField] private VetrickControll vetrickControll;

        protected override void RegisterDependencies()
        {
            container.RegisterInstance<EntryPoint>(this);
            container.RegisterInstance<VetrickControll>(vetrickControll);
            container.RegisterInstance<STColorValidator>(colorValidator);
            container.RegisterInstance<STHistoryColor>(historyColor);
            container.RegisterInstance<STSimonWheel>(simonWheel);
            container.RegisterInstance<STUiView>(uiView);
            container.RegisterInstance<STGameController>(gameController);
            container.RegisterInstance<STBuilderGame>(builderGame);
            var gameSettingsManager = new STGameSettingsManager();
            container.RegisterInstance<STGameSettingsManager>(gameSettingsManager);
        }
    }
}
