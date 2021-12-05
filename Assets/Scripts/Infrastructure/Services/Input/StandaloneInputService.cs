using UnityEngine;

namespace Infrastructure.Services.Input
{
    public class StandaloneInputService : InputService
    {
        public override Vector2 Axis
        {
            get
            {
                var input = GetSimpleInputAxis();
                if (input == Vector2.zero)
                {
                    input = GetUnityAxis();
                }
                return input;
            }
        }

        private static Vector2 GetUnityAxis() => 
            new Vector2(UnityEngine.Input.GetAxis(Horizontal), UnityEngine.Input.GetAxis(Vertical));
    }
}