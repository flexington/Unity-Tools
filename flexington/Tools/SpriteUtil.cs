using UnityEngine;

namespace flexington.Tools
{
    public static class SpriteUtil
    {
        /// <summary>
        /// Returns a rect describing the position of the sprite in the current world.
        /// Use xMin, yMin and xMax and yMax of the returned rect to get the 2D world coorindinates of the sprite.
        /// </summary>
        public static Rect GetWorldRect(Transform transform, Sprite sprite)
        {
            float ppu = sprite.pixelsPerUnit;

            Rect rect = sprite.rect;
            rect.xMax /= ppu;
            rect.yMax /= ppu;
            Vector2 pivot = sprite.pivot / ppu;

            float x = rect.xMax * pivot.x;
            float y = rect.yMax * pivot.y;

            Rect spriteRect = new Rect();
            spriteRect.xMin = transform.position.x - x;
            spriteRect.xMax = transform.position.x + x;
            spriteRect.yMin = transform.position.y - y;
            spriteRect.yMax = transform.position.y + y;

            return spriteRect;
        }

        /// <summary>
        /// Converts the given WorldPosition into the corresponding pixel position of the given sprite.
        /// </summary>
        /// <param name="worldPosition">The world position as a Vector2</param>
        /// <param name="transform">The transform of the game object that holds the sprite</param>
        /// <param name="sprite">The spite iteself</param>
        /// <returns>The X,Y coordinates of the closest pixel</returns>
        /// <todo>The actual selected Pixel is one pixel above the curser</todo>
        public static Vector2 WorldToPixelCoordinates(Vector2 worldPosition, Transform transform, Sprite sprite)
        {
            // Change coordinates to local coordinates of this image
            Vector3 localPosition = transform.InverseTransformPoint(worldPosition);

            // Change these to coordinates of pixels
            float pixelWidth = sprite.rect.width;
            float pixelHeight = sprite.rect.height;
            float unitsToPixels = pixelWidth / sprite.bounds.size.x * transform.localScale.x;

            // Need to center our coordinates
            float centered_x = localPosition.x * unitsToPixels + pixelWidth / 2;
            float centered_y = localPosition.y * unitsToPixels + pixelHeight / 2;

            // Round current mouse position to nearest pixel
            Vector2 pixel_pos = new Vector2(Mathf.RoundToInt(centered_y), Mathf.RoundToInt(centered_x));

            return pixel_pos;
        }
    }
}