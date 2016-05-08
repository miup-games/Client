#if UNITY_EDITOR
using System;
using System.Collections;
using System.Diagnostics;
using MIUP.Infraestructure.MIUPUtilsModule;
using UnityEditor;
using UnityEngine;


namespace MIUP.GameName.CardModule.Application
{
    [ExecuteInEditMode]
    public class CardDecorator : MonoBehaviour
    {
        [SerializeField]  private bool snapX = true;
        [SerializeField]  private bool snapY = false;
        [SerializeField]  private bool snapZ = false;

        [SerializeField]  private CardSnapMode cardSnapModeX = CardSnapMode.Width;
        [SerializeField]  private CardSnapMode cardSnapModeY = CardSnapMode.Width;
        [SerializeField]  private CardSnapMode cardSnapModeZ = CardSnapMode.Width;

        [SerializeField]  private Vector3 cubeSize = Vector3.one;
        [SerializeField]  private Vector3 originPosition = Vector3.zero;

        void Awake()
        {
            
        }

        void Update()
        {
            Snap();      
        }           

        private void Snap()
        {
            float xMultiplier = GetSnapedPosition(this.transform.position.x, originPosition.x, cardSnapModeX);
            float yMultiplier = GetSnapedPosition(this.transform.position.y, originPosition.y, cardSnapModeY);
            float zMultiplier = this.transform.position.z;//GetSnapedPosition(this.transform.position.z, originPosition.z, cardSnapModeZ);

            UnityEngine.Debug.LogError("xMultiplier "+xMultiplier);  
            UnityEngine.Debug.LogError("yMultiplier "+yMultiplier);  
            UnityEngine.Debug.LogError("zMultiplier "+yMultiplier);  

            this.transform.position = new Vector3(xMultiplier, yMultiplier, zMultiplier);
        }          

        private float GetSnapedPosition(float currentPosition, float basePosition, CardSnapMode snapMode)
        {
            int multiplier = MIUPMathUtils.GetIntDivisionResult(currentPosition, basePosition);   
            int sign = currentPosition > 0 ? 1 : -1;

            switch(snapMode)
            {
                case CardSnapMode.HalfWidth:
                    return (multiplier) + basePosition;

                default:
                    return sign*(Math.Abs(multiplier) + Math.Abs(basePosition));
            }

        }
    }


}
#endif
