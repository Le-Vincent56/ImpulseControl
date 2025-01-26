using ImpulseControl.Input;
using UnityEngine;

namespace ImpulseControl
{
    public class PlayerMovement : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private GameInputReader gameInputReader;
        [SerializeField] private Rigidbody2D rigidbody2d;

        [Header("Attributes")] 
        [SerializeField] private float speed = 1.0f;

        private void Awake()
        {
            // Get components
            rigidbody2d = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate()
        {
            Move(gameInputReader.NormMoveX, gameInputReader.NormMoveY);
        }
        
        private void Move(float xNorm, float yNorm)
        {
            rigidbody2d.velocity = new Vector2(xNorm * speed, yNorm * speed);
        }
    }
}
