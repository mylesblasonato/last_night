using Fungus;
using MDI.Gamekit.Core;
using UnityEngine;

namespace Game.GameProductionOne
{
    public class HotspotManager : MonoSingleton<HotspotManager>
    {
        #region SFIELDS
        [SerializeField] Flowchart[] yearFlows;
        [SerializeField] string yearFlowBlockToExecute = "Speech";
        [SerializeField] Transform[] years;
        [SerializeField] SEvent OnNoMoreRemaining;
        [SerializeField] int currentYear = 0;
        #endregion

        bool hasRemaining = true;
        HotspotController[] controllers;

        void Awake()
        {
            CreateSingleton(this, () => Destroy(this.gameObject));
        }

        #region ABSTRACTION
        public void PoloroidsRemaining()
        {          
            foreach (HotspotController hotspot in controllers)
            {
                if (hotspot.gameObject.activeSelf)
                {
                    hasRemaining = true;
                    break;
                }
                else
                {
                    hasRemaining = false;
                }
            }

            if (!hasRemaining)
            {
                foreach (Transform hotspot in years[currentYear])
                    Destroy(hotspot.gameObject);
                yearFlows[currentYear].ExecuteBlock(yearFlowBlockToExecute);
                currentYear++;
                ActivateYear(currentYear);
            }
        }

        public void ActivateYear(int yearIndex)
        {
            hasRemaining = true;
            foreach (var year in years)
                year.gameObject.SetActive(false);
            years[yearIndex].gameObject.SetActive(true);
            controllers = years[yearIndex].GetComponentsInChildren<HotspotController>();
        }
        #endregion
    }
}
