namespace CrazyHub.Hyderabad.Assignment
{
    using UnityEngine;
    using UnityEditor;
    using System;
    using System.Collections.Generic;
    using UnityEditor.Graphs;
    using UnityEditor.SceneManagement;

    public class LevelEditor : EditorWindow {
        Vector2 offset;
        Vector2 drag;

        List<List<Node>> nodes;
        List<List<PartScript>> Parts;

        GUIStyle empty;
        Vector2 nodePos;

        StyleManager stylemanger;
        private bool isErasing;
        private Rect MenuBar;
        private GUIStyle currentStyle;
        private GameObject TheMap;

        [MenuItem("Window/LevelEditor")]
        public static void OpenWindow() {
            LevelEditor window = GetWindow<LevelEditor>();
            window.titleContent = new GUIContent("ObjectSpawner");
        }

        private void OnEnable() {           
            setUpStyles();
            setUpNodesAndParts();
            SetUpMap();
            //Texture2D icon = Resources.Load("IconTex/Empty") as Texture2D;
            //empty = new GUIStyle();      
            //empty.normal.background = icon;
        }

        

        private void SetUpMap() {
            try {
                TheMap = GameObject.FindGameObjectWithTag("Map");
                RestoreTheMap(TheMap);
            } catch (Exception) {
                Debug.LogError("Error: Check for tag in the Map Object");

            }

            if (TheMap == null) {
                TheMap = new GameObject("Map");
                TheMap.tag = "Map";
            }
        }

        private void RestoreTheMap(GameObject theMap) {
           if(theMap.transform.childCount > 0) {
                for (int i = 0; i < theMap.transform.childCount; i++) {
                    int row = theMap.transform.GetChild(i).GetComponent<PartScript>().Row;
                    int column = theMap.transform.GetChild(i).GetComponent<PartScript>().Column;
                    GUIStyle theStyle = theMap.transform.GetChild(i).GetComponent<PartScript>().style;
                    nodes[row][column].setStyle(theStyle);
                    Parts[row][column] = theMap.transform.GetChild(i).GetComponent<PartScript>();
                    Parts[row][column].Part = theMap.transform.GetChild(i).gameObject;
                    Parts[row][column].name = theMap.transform.GetChild(i).name;
                    Parts[row][column].Row = row;
                    Parts[row][column].Column = column;
                }
            }
        }

        private void setUpStyles() {

            try {
                stylemanger = GameObject.FindGameObjectWithTag("StyleManager").GetComponent<StyleManager>();
                for (int i = 0; i < stylemanger.buttonStyles.Length; i++) {
                    stylemanger.buttonStyles[i].NodeStyle = new GUIStyle();
                    stylemanger.buttonStyles[i].NodeStyle.normal.background = stylemanger.buttonStyles[i].Icon;
                }

            } catch (Exception) {
                Debug.LogError("Error in Style Manager. Check for tag in the StyleManager Object");
            }

            empty = stylemanger.buttonStyles[0].NodeStyle;
            // Initialize CurrentStyle
            currentStyle = stylemanger.buttonStyles[1].NodeStyle;


        }

        private void setUpNodesAndParts() {
            nodes = new List<List<Node>>();
            Parts = new List<List<PartScript>>();
            for (int i = 0; i < 20; i++) {

                nodes.Add(new List<Node>());
                Parts.Add(new List<PartScript>());
                for (int j = 0; j < 10; j++) {

                    nodePos.Set(i * 30, j * 30);
                    nodes[i].Add(new Node(nodePos, 30, 30, empty));
                    Parts[i].Add(null);
                }
            }
        }

        private void OnGUI() {
            DrawGrid();
            DrawNodes();
            DrawMenuBar();
            OnPressingNode(Event.current);
            DragGrid(Event.current);
            if (GUI.changed) {
                Repaint();
            }
        }

        private void DrawMenuBar() {
            MenuBar = new Rect(0, 0, position.width, 20);
            GUILayout.BeginArea(MenuBar, EditorStyles.toolbar);
            GUILayout.BeginHorizontal();

            for (int i = 0; i < stylemanger.buttonStyles.Length; i++) {
                if (GUILayout.Toggle((currentStyle == stylemanger.buttonStyles[i].NodeStyle), new GUIContent(stylemanger.buttonStyles[i].ButtonText), EditorStyles.toolbarButton, GUILayout.Width(80))) {
                    currentStyle = stylemanger.buttonStyles[i].NodeStyle;
                }
            }

            GUILayout.EndHorizontal();
            GUILayout.EndArea();

            //if ( GUILayout.Toggle((currentStyle == stylemanger.buttonStyles[1].NodeStyle), new GUIContent("Chicken"), EditorStyles.toolbarButton, GUILayout.Width(80))) {
            //    currentStyle = stylemanger.buttonStyles[1].NodeStyle;
            //}

            //if (GUILayout.Toggle((currentStyle == stylemanger.buttonStyles[2].NodeStyle), new GUIContent("Cow"), EditorStyles.toolbarButton, GUILayout.Width(80))) {
            //    currentStyle = stylemanger.buttonStyles[2].NodeStyle;
            //}

            //if (GUILayout.Toggle((currentStyle == stylemanger.buttonStyles[3].NodeStyle), new GUIContent("Lamb"), EditorStyles.toolbarButton, GUILayout.Width(80))) {
            //    currentStyle = stylemanger.buttonStyles[3].NodeStyle;
            //}

            //if (GUILayout.Toggle((currentStyle == stylemanger.buttonStyles[4].NodeStyle), new GUIContent("tree"), EditorStyles.toolbarButton, GUILayout.Width(80))) {
            //    currentStyle = stylemanger.buttonStyles[4].NodeStyle;
            //}

        }

        private void OnPressingNode(Event e) {

            int row = (int)((e.mousePosition.x - offset.x) / 30);
            int col = (int)((e.mousePosition.y - offset.y) / 30);

            if ((e.mousePosition.x - offset.x) < 0 || (e.mousePosition.x - offset.x) > 600 || (e.mousePosition.y - offset.y) < 0 || (e.mousePosition.y - offset.y) > 300) {

            } else {
                if (e.type == EventType.MouseDown) {
                    if (nodes[row][col].style.normal.background.name == "Empty") {
                        isErasing = false;
                    } else {
                        isErasing = true;
                    }
                    PaintNodes(row, col);
                    if (e.type == EventType.MouseDrag) {
                        PaintNodes(row, col);
                        e.Use();
                    }
                }
            }
        }

        public void PaintNodes(int row, int col) {

            // If new item is added then set it true;
            bool ItemAdded = false;           

            if (isErasing) {
                if (Parts[row][col] != null) {
                    nodes[row][col].setStyle(empty);
                    DestroyImmediate(Parts[row][col].Part.gameObject);
                    GUI.changed = true;
                }
                Parts[row][col] = null;
            } else {
                // nodes[row][col].setStyle(stylemanger.buttonStyles[1].NodeStyle);
                nodes[row][col].setStyle(currentStyle);
                GameObject gObj = Instantiate(Resources.Load("MapParts/"+currentStyle.normal.background.name)) as GameObject;
                gObj.name = currentStyle.normal.background.name;
                gObj.transform.position = new Vector3(col * 10, 0, row * 10);
                gObj.transform.parent = TheMap.transform;

                Parts[row][col] = gObj.GetComponent<PartScript>();
                Parts[row][col].Part = gObj;
                Parts[row][col].PartName = gObj.name;
                Parts[row][col].Row = row;
                Parts[row][col].Column = col;
                Parts[row][col].style = currentStyle;
                GUI.changed = true;
                ItemAdded = true;
            }
            
            // Marking the Map Parent Obj as Dirty. so that we can save all the child it has.
            if (ItemAdded) //If there is no script, get it
            {
                GameObject objMap = GameObject.FindGameObjectWithTag("Map").gameObject;
                EditorUtility.SetDirty(objMap); //Save changes
                EditorSceneManager.MarkSceneDirty(objMap.gameObject.scene); //changes can be saved
            } 
        }

        private void DrawNodes() {
            for (int i = 0; i < 20; i++) {

                for (int j = 0; j < 10; j++) {
                    if (nodes[i][j] != null)
                        nodes[i][j].Draw();
                    else
                        Debug.LogError("Nodes is null" +" "+ i +" "+ j);
                }
            }
        }

        private void DragGrid(Event e) {
            drag = Vector2.zero;

            switch (e.type) {
                case EventType.MouseDrag:
                    if (e.button == 0) {
                        OnMouseDrag(e.delta);
                    }
                    break;
            }
        }

        private void OnMouseDrag(Vector2 delta) {
            drag = delta;

            for (int i = 0; i < 20; i++) {

                for (int j = 0; j < 10; j++) {
                    nodes[i][j].OnDrag(delta);
                }
            }

            GUI.changed = true;
        }

        public void DrawGrid() {
            int widthCount = Mathf.CeilToInt(position.width / 20);
            int heightCount = Mathf.CeilToInt(position.height / 20);

            Handles.BeginGUI();

            Handles.color = Color.cyan;
            offset += drag;
            Vector3 newOffset = new Vector3(offset.x % 20, offset.y % 20, 0);

            for (int i = 0; i < widthCount; i++) {
                Handles.DrawLine(new Vector3(20 * i, -20, 0) + newOffset, new Vector3(20 * i, position.height, 0) + newOffset);
            }

            for (int i = 0; i < heightCount; i++) {
                Handles.DrawLine(new Vector3(-20, 20 * i, 0) + newOffset, new Vector3(position.width, 20 * i, 0) + newOffset);
            }

            Handles.color = Color.white;
            Handles.EndGUI();
        }
    }
}





