using System.Collections.Generic;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;

[CreateAssetMenu(fileName = "Texture Dict", menuName = "ScriptableObjects/SpawnTextureDictSO", order = 2)]
public class TextureDictionaryScriptableObject : ScriptableObject
{
    public SerializableDictionaryBase<string, Texture> TextureDict;
}
