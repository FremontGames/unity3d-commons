﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

class GameSceneView
{
    public GameObject UICanvas;
    public GameObject BackgroundSprite;

    public GameObject ScoreValue;
    public GameObject Completion;
    public GameObject CompletionValue;

    public GameObject UndoButton;
    public GameObject QuitDialog;
    public GameObject QuitButton;
    public GameObject QuitConfirmButton;
    public GameObject QuitCancelButton;

    public Dictionary<string, GameObject> TileObjects;
    public Dictionary<string, SpriteRenderer> GameMoves;
}
