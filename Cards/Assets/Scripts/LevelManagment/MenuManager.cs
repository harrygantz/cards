using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

namespace DEAL.LevelManagement
{
    public class MenuManager : MonoBehaviour
    {
        [SerializeField]
        private MainMenu mainMenuPrefab;
        [SerializeField]
        private SettingsMenu settingsMenuPrefab;
        [SerializeField]
        private LeaderboardsMenu LeaderboardMenuPrefab;
        [SerializeField]
        private GameMenu GameMenuPrefab;
        [SerializeField]
        private PauseMenu PauseMenuPrefab;
        [SerializeField]
        private WinScreen WinScreenPrefab;

        [SerializeField]
        private Transform _menuParent;
        
        private Stack<Menu> _menuStack = new Stack<Menu>();

        private static MenuManager _instance;

        public static MenuManager Instance{ get { return _instance; } }

        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                _instance = this;
                InitializedMenus();
                DontDestroyOnLoad(gameObject);
            }
        }

        private void OnDestroy()
        {
            if (_instance == this)
            {
                _instance = null;
            }
        }

        private void InitializedMenus()
        {
            if (_menuParent == null)
            {
                GameObject menuParentObject = new GameObject("Menus");
                _menuParent = menuParentObject.transform;
            }
            
            DontDestroyOnLoad(_menuParent.gameObject);

            // Here we are getting the game manager type and storing it in a reference myType. 
            System.Type myType = this.GetType();
            
            // This is a special enumeration that determines how you search through a reflection. We use BindingFlags.Instance
            // which initiates our search using nonstatic fields. using the bitwise or operator we also are searching for 
            // private fields. Finally we also want the fields we specifically have in MenuManager and not from inheritance so
            // we will all use DeclaredOnly
            BindingFlags myFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly;
            
            // We are using System.Reflection this namespace helps us get type information at runtime. GetFields() returns
            // information about each field and returns it using a class called FieldInfo.
            FieldInfo[] fields = myType.GetFields(myFlags);
            
            foreach (FieldInfo field in fields)
            {
                // Used to locate the prefab object from each field. GetValue() retuns whatever is stored in each field.
                // For the menu fields this should return a menu prefab game object.
                Menu prefab = field.GetValue(this) as Menu;
                
                if (prefab != null)
                {
                    Menu menuInstance = Instantiate(prefab, _menuParent);
                    
                    if (prefab != mainMenuPrefab)
                    {
                        menuInstance.gameObject.SetActive(false);
                    }
                    else
                    {
                        // By placing OpenMenu here we are guaranteeing that the main menu is the first on the stack
                        OpenMenu(menuInstance);
                    }
                }
            }
        }
        
        // <i>SUMMARY<i>
        // We are using a stack data structure to control which menu is visable ans which menu is not visible (active/inactive)
        public void OpenMenu(Menu menuInstance)
        {
            if (menuInstance == null)
            {
                Debug.LogWarning("MENUMANAGER OpenMenu ERROR: Invalid menu");
                return;
            }

            if (_menuStack.Count > 0)
            {
                foreach (Menu menu in _menuStack)
                {
                    menu.gameObject.SetActive(false);
                }
            }
            
            menuInstance.gameObject.SetActive(true);
            _menuStack.Push(menuInstance);
        }

        public void CloseMenu()
        {
            if (_menuStack.Count == 0)
            {
                Debug.LogWarning("MENUMANAGER CloseMenu ERROR: No menus in stack");
                return;
            }

            Menu topMenu = _menuStack.Pop();
            topMenu.gameObject.SetActive(false);

            if (_menuStack.Count > 0)
            {
                Menu nextMenu = _menuStack.Peek();
                nextMenu.gameObject.SetActive(true);
            }
        }
    }    

}

