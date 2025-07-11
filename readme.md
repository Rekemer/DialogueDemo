## Unity Dialogue State Machine

| Feature | Details |
|---------|---------|
| **State-pattern core** | `BaseFrameState` + concrete states (`DialogueState`, `TextState`, `OptionState`, `FinalState`) keep logic self-contained and reusable. |
| **Data-driven** | Dialogues are described by a single JSON file (`Stories/story.json`)
| **Assembly Definitions** | Code is divided live in their own asmdef, speeding up compile times. |

| |  |
|----|----|
![](Screenshots/choice.gif) | ![](Screenshots/Screenshot_1.png) |
 ![](Screenshots/Screenshot_2.png) | ![](Screenshots/Screenshot_3.png) 
