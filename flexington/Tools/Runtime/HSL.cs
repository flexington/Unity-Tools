// http://www.niwa.nu/2013/05/math-behind-colorspace-conversions-rgb-hsl/

using System;
using UnityEngine;

namespace flexington.Tools
{
    public struct HSL
    {
        public float h;
        public float s;
        public float l;

        public HSL(float h, float s, float l)
        {
            this.h = h;
            this.s = s;
            this.l = l;
        }

        public HSL(Color color)
        {
            h = 0;
            s = 0;
            l = 0;

            HSL hsl = FromColor(color);

            h = hsl.h;
            s = hsl.s;
            l = hsl.l;
        }

        public HSL FromColor(Color color)
        {
            return FromRGB(color.r, color.g, color.b);
        }

        public HSL FromRGB(float r, float g, float b)
        {
            float min = Mathf.Min(Mathf.Min(r, g), b);
            float max = Mathf.Max(Mathf.Max(r, g), b);

            float h = 0;
            float s = 0;
            float l = (max + min) / 2f;

            if (l < 0.5f) s = (max - min) / (max + min);
            else s = (max - min) - (2f - max - min);

            if (r == max) h = (g - b) / (max - min);
            else if (g == max) h = 2f + (b - r) / (max - min);
            else if (b == max) h = 4f + (r - g) / (max - min);

            return new HSL(h, s, l);
        }

        public Color ToRGB()
        {
            // If saturation = 0, assign luminance to all colors
            if (s == 0) return new Color(l, l, l);

            Color color = new Color();

            float v1, v2;

            if (l < 0.5f) v1 = l * (1f + s);
            else v1 = l + s - l * s;
            v2 = 2f * l - v1;

            float hue = h / 6f;

            color.r = hue + (1f / 3f);
            color.r = HueToTGB(v1, v2, color.r);

            color.g = hue;
            color.g = HueToTGB(v1, v2, color.g);

            color.b = hue - (1f / 3f);
            color.b = HueToTGB(v1, v2, color.b);

            return color;
        }

        private float HueToTGB(float v1, float v2, float vH)
        {
            if (vH > 1) vH -= 1;
            else if (vH < 0) vH += 1;

            if (6 * vH < 1) return v2 + (v1 - v2) * 6 * vH;
            if (2 * vH < 1) return v1;
            if (3 * vH < 2) return v2 + (v1 - v2) * (2f / 3f - vH) * 6;
            return v2;
        }
    }
}