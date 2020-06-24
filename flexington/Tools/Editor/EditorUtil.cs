using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace flexington.Tools
{
    public static class EditorUtil
    {
        private static Dictionary<string, Texture2D> _icons;
        public static Dictionary<string, Texture2D> Icons
        {
            get
            {
                if (_icons == null)
                {
                    _icons = new Dictionary<string, Texture2D>();
                    _icons.Add("Visible", TextureUtil.FromBase64("iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAABKElEQVQ4T7XTvyuuYRgH8M87yHKSMpFfi+wyGCiLSVkYrIrFRET5A3SGw+kkSdmsBoPFxsBC/gHlHA6TDMQioUvXW2/Pgt7eq56ep+e+ru+v+75LqqxSlfNqBtCIXrSkwhuc4r6ouKigH0sYwlU+r2hHJ/axjOMyUBmgHmsYx29s4T86+LD5D62Ywiy28/0cizG8hwaM5WAQBNBMMq1gPr9DzQ7uMBIAG+l3EE/ZFMzBWlltuM4fP3CAowD4gwEEwEM2hN+/FdNvCIAIMypCPoxMAqAOu2hOCxfZ9AtziOGfGW4sdaWFIBgthxggq5jAeoZ4nsEFQDB3YxLT2MQCXorb2IdFDOMWlxWWmjLsUHNS3Mbi+YiQegoKzvD42UH69tWo2V34spJ3uGk6NEgTRaoAAAAASUVORK5CYII="));
                    _icons.Add("Hidden", TextureUtil.FromBase64("iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAYAAAAf8/9hAAABWUlEQVQ4T72SO0tDQRCFz85uYyFIsBN8FlaiZezS2CriHxAsE+7mFtY+SsFsSNIr9vEXSAIW2ouFjTHpLGyuoF1mZC/ZEPWKRSBb73znzJyjMOZTY85jsgDn3KJ3HMdxNzjPdFCpVOaMMQUACwBIRO6Z+Vlrfdnv9/f/BDjnlrXW58y8Q0QpnJlfrLUr9Xq9LSInzNyL47jzy0Gj0dhi5iaA6cHgtVLqSETeiehKKXUqIhcAZgDsRVF04/+lKrVabR3APYCpQBaRNSKaFZFjAAcAWgDmB/BPAJvlcvkhBTjnNrTWdwHgbQMoeOUoigrVanWViJ5GIv8Qkby19nF4xLACM7+Fnb0yMxsiOgOwPVBPlFK71tr2cIVA9kdMkqSby+VaIzuntv09RaSptT4slUq9zBh9ziEqIloCkCciYeaOMea2WCy+/mzutx5kFeW/qk+2ylluxnbwBSn/nxG+dOmcAAAAAElFTkSuQmCC"));
                    _icons.Add("Questionmark", TextureUtil.FromBase64("iVBORw0KGgoAAAANSUhEUgAAADAAAAAwCAYAAABXAvmHAAAFIklEQVRoQ8Waa6imUxTHf5NSZDI++KQYmlLkfqmZUsZ8UeRSlFwGURKjkWYyMe4ikjsRuUtRSpQvbikK4xql5FY++YCIUqLfae9pn/3u53n2fp73HKtO7+mcvdde/73X+u+113pXMH85HNgFiJ+u8A/wafI5t1VXzEHTgcAZwEnA2kp97wOvAS8BX1fOKQ4bC2BX4NLwI4ApIoCHgUeBv1oVjQFwAXAjsG/HYqmr6DpK6lK6Vkl+BK4HnmwB0QLgWOCR4NvpGj8DrwR3eL1y8ROD250C7J3NcQMuAT6o0VUL4GzgMWC3RKk7dlUwvGatrjHGz13ZiepKgnhmSHENgNuAqxNFfwJXAI8PKW/8/0XAfcDuybzbgW19evoA6LcvhKOOOj4HLgY+bDSudvgxwU2PSCbIVGcFCp7R0wcg3/nngHNrLZk47lngnJqT6AKgz2twFFnnholGtU53PVkpioCez5WUAMg2bycBu5w7n9uXnoSBfXzOTjkAL6gvgTVBkz5/WOPWnQacCqxOKFdq/BV4J/C8v9fKZ8ChYfA3wMHA33FyDkC20fcV2UbEtQHrBXV3mNNnnMbf2+CSBrYeEdlJVpKdFiQFsCfwA+CncmYDx2v8W8Cq2m0NRq2vHO9d8WIY+xuwH+DnIgDp7n+RHNvQGhr9XaPxUec9wJVDCyTufEj4fecpxBOQ879NbsOTQ7ZYoztnizhHv38K+D7EhDlUSfYPY4bWMtt9NQwyCzjAuyECOAF4I/mnR1QrvxR2X5/N3aML6IUNCZwuHpPIDcCbEcD9wOXB4psy/u0DYpDr+7l4k3oCqchKulouLXeMY68LCiSCzRGAdBn9q7R4Fwjd4onsn7LMXh0T/p0I4Cjgo6BjIU4FsAfwe/jjH8DKWt8BBHB+Nt6dLwWm98PLBd2ONZhrRVu1WVkpAJ+B74U/+NRbV6upYVwfzdYGcVxOW+PTdZ0A0rzHXCNNohps7BxqnLjzpTvC15dB3CJPA+eFCRsFYPAaxMoDwKYWbQNjSzESp+hqMlVLWuHclHA2CSC9wAYfEA3guhhKFdLs6SOMd26a5m9bSgBSptSZSwttlvZrBsBSuJBB+0lh9ZZLq+uwZ1xoKYJ4c8hMUyPGBGwJxEwQLwWNltKGeey+gGZo1PQ5MoEpaktK3HXMJQAyjsE7VbQ1pvyrpqYSU41pnZ/G1s5UIufWlmSuzwBP0gXdsTyxazU8jk+TuYU7ax7pdMmYPHWYwvup/s50esqDpgRACs2LuK1JW66390Hj4GuAW8KslidlCcDUtLmkM035rwVuddC8HvX5gvM+gapHvUbkZZXjgI9HRFweA2MTN5c+Eni3pqzi4Lyw5U6qYIxEFnLuFP53A2Oxd7Cw5WKWFkUsGMWrO391jQE0Zo5VjY1hotU4PWJR46OruOuDQcOj+JC+eYwFE+ZsB7yToghkpuHRV16/A9iSKFjOIm9eXr8T2FrajKEGh+U8Hx5RdoTOZG29tPUAjDf7cEcnE32OWuaMDcNFOodaTF5w3g15i+myhmJULQifnw8WWkxyftF4FQ8BiIur/KFCk8+8v1QqqTXacZ6wZZW0bWvA2uQbbLnWAojsZBHroMy6nwDbq/ayWtustlv3yfR9FSoVc22zpmvYTZSVSo3u/DsRXY1uXTMXC7ayTlP3s+UE0gXtF/tVA9/TFqamiI9/U2O/brAsXzXIjY1f9rDr7iVYI7KZsfO/fdmjy8j8OxHRVbpcqwZs75j/AMktIE9/K6P7AAAAAElFTkSuQmCC"));
                    _icons.Add("Search", TextureUtil.FromBase64("iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAQAAAC1+jfqAAAABGdBTUEAALGPC/xhBQAAACBjSFJNAAB6JgAAgIQAAPoAAACA6AAAdTAAAOpgAAA6mAAAF3CculE8AAAAAmJLR0QAAKqNIzIAAAAJcEhZcwAADsQAAA7EAZUrDhsAAAAHdElNRQfkBBsKBiK9NObeAAAA2UlEQVQoz4XOMUoDQRQG4C+pRLENLOQCsQukVNJ4gRTpJCQXEFN5AK31ALIXEFIGTGnjFRbstAqKBCVFsrCOhbu62YD+08zP++YxQE3fzJuVR9eaKtk3Fby6FXuQWeqVxzVTny7s5L0tker+gr7gcmNjZC5RL+rMy8/rImPB0fe1ruPeqgLu0CnAno/qr71jtwDPWlvgAE9FuZJpV8BEqlGUpqVEVBqfCuKy70nNjbVEjk0EwdqwTLoSIT+p2FqQGZVJ3aEz5040MJRtk82McjL4jyz8kYGFmy85wkTZ8zKtPwAAACV0RVh0ZGF0ZTpjcmVhdGUAMjAyMC0wNC0yN1QxMDowNjozNCswMDowMCbgsWMAAAAldEVYdGRhdGU6bW9kaWZ5ADIwMjAtMDQtMjdUMTA6MDY6MzQrMDA6MDBXvQnfAAAAGXRFWHRTb2Z0d2FyZQB3d3cuaW5rc2NhcGUub3Jnm+48GgAAAABJRU5ErkJggg=="));
                    _icons.Add("Add", TextureUtil.FromBase64("iVBORw0KGgoAAAANSUhEUgAAABAAAAAQCAQAAAC1+jfqAAAABGdBTUEAALGPC/xhBQAAACBjSFJNAAB6JgAAgIQAAPoAAACA6AAAdTAAAOpgAAA6mAAAF3CculE8AAAAAmJLR0QAAKqNIzIAAAAJcEhZcwAADsQAAA7EAZUrDhsAAAAHdElNRQfkBBsKDyn7JIQfAAAAcklEQVQoz72RQQqAIBBFX1LHyMO0KGjTOdx0tzZBnSQ9SRA0bUQUzE3QG/igPPgwAzEah6PlFYMgmPhLJUITZVbI8F2o0fRB63ye/n2zg0MKYxVSbJCKliFUjEzAwhoqttSfEYT55z2kwhVlFo3lSM/9AH/RJPcXeIy8AAAAJXRFWHRkYXRlOmNyZWF0ZQAyMDIwLTA0LTI3VDEwOjE1OjQxKzAwOjAwVKTzHgAAACV0RVh0ZGF0ZTptb2RpZnkAMjAyMC0wNC0yN1QxMDoxNTo0MSswMDowMCX5S6IAAAAZdEVYdFNvZnR3YXJlAHd3dy5pbmtzY2FwZS5vcmeb7jwaAAAAAElFTkSuQmCC"));
                }
                return _icons; 
            }
        }

        // public static void DrawTexture(Rect position, Sprite sprite, Vector2 size)
        // {
        //     Rect spriteRect = new Rect(sprite.rect.x / sprite.texture.width, sprite.rect.y / sprite.texture.height,
        //                                sprite.rect.width / sprite.texture.width, sprite.rect.height / sprite.texture.height);
        //     Vector2 actualSize = size;

        //     actualSize.y *= (sprite.rect.height / sprite.rect.width);
        //     Graphics.DrawTexture(new Rect(position.x, position.y + (size.y - actualSize.y) / 2, actualSize.x, actualSize.y), sprite.texture, spriteRect, 0, 0, 0, 0);
        // }

        // public static void DrawTextureGUI(Rect position, Sprite sprite, Vector2 size)
        // {
        //     Rect spriteRect = new Rect(sprite.rect.x / sprite.texture.width, sprite.rect.y / sprite.texture.height,
        //                                sprite.rect.width / sprite.texture.width, sprite.rect.height / sprite.texture.height);
        //     Vector2 actualSize = size;

        //     actualSize.y *= (sprite.rect.height / sprite.rect.width);
        //     GUI.DrawTextureWithTexCoords(new Rect(position.x, position.y + (size.y - actualSize.y) / 2, actualSize.x, actualSize.y), sprite.texture, spriteRect);
        // }
    }
}