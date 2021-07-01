using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    This script was created to launch function calls via keyboard shortcuts.
    Ideally these should be removed at some point.
*/
public class DebugKeys : MonoBehaviour {
    void Update() {
      // Trigger Save on CTRL+S
      if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.S)) {
        GameState.SaveState("./save.bin");
      }

      // Trigger Load on CTRL+O
      if (Input.GetKey(KeyCode.LeftControl) && Input.GetKeyDown(KeyCode.O)) {
        GameState.LoadState("./save.bin");
      }
    }
}
