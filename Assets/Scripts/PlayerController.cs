using UnityEngine;
using System;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D body;
    public SpriteRenderer spriteRenderer;

    // Sprites for primary directions
    public List<Sprite> nSprites;
    public List<Sprite> neSprites;
    public List<Sprite> eSprites;
    public List<Sprite> seSprites;
    public List<Sprite> sSprites;
    public List<Sprite> swSprites;
    public List<Sprite> wSprites;  
    public List<Sprite> nwSprites; 

    public float walkSpeed = 5f;
    public float frameRate = 8f;

    private float idleTime;
    private Vector2 direction;

    void Update()
    {
        // Get movement input
        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");

        // Debugging input direction
        Debug.Log("Horizontal: " + direction.x + ", Vertical: " + direction.y);

        // Update velocity
        if (direction.magnitude > 0.1f)
        {
            body.linearVelocity = direction.normalized * walkSpeed;
            Debug.Log("Moving: Velocity = " + body.linearVelocity);
        }
        else
        {
            body.linearVelocity = Vector2.zero;
            Debug.Log("Idle: Velocity = " + body.linearVelocity);
        }

        // Handle sprite animation
        SetSprite();
    }

    void SetSprite()
    {
        List<Sprite> directionSprites = GetSpriteDirection();

        if (directionSprites != null && directionSprites.Count > 0)
        {
            float playTime = Time.time - idleTime;
            int totalFrames = (int)(playTime * frameRate);
            int frame = totalFrames % directionSprites.Count;
            spriteRenderer.sprite = directionSprites[frame];

            // Debugging sprite frame
            Debug.Log("Sprite Frame: " + frame);
        }
        else
        {
            idleTime = Time.time; // Reset idle timer when not moving
            Debug.Log("Idle: No Sprite Animation");
        }
    }

    List<Sprite> GetSpriteDirection()
    {
        List<Sprite> currentSprites = null;

        // Handling the 8 main directions
        if (direction.y > 0.1f)
        {
            if (Mathf.Abs(direction.x) > 0.1f)
                currentSprites = neSprites;  // North-East
            else
                currentSprites = nSprites;  // North
        }
        else if (direction.y < -0.1f)
        {
            if (Mathf.Abs(direction.x) > 0.1f)
                currentSprites = seSprites;  // South-East
            else
                currentSprites = sSprites;  // South
        }
        else
        {
            if (Mathf.Abs(direction.x) > 0.1f)
            {
                if (direction.x > 0)
                    currentSprites = eSprites;  // East
                else
                    currentSprites = wSprites;  // West
            }
        }

        // Handle opposite directions (left and right)
        if (direction.x < -0.1f && direction.y > 0.1f) currentSprites = nwSprites;  // North-West
        if (direction.x < -0.1f && direction.y < -0.1f) currentSprites = swSprites;  // South-West

        // Debugging selected sprite direction
        Debug.Log("Selected Sprite Direction: " + (currentSprites != null ? currentSprites[0].name : "None"));

        return currentSprites;
    }
}
