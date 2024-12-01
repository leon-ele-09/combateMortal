using UnityEngine;
using UnityEngine.UI;

public class SeleccionDePersonajes : MonoBehaviour
{
    public static SeleccionDePersonajes Instance { get; private set; }

    // Estadísticas base (EntityData)
    public string nombrePersonaje;
    public int vidaMaxima;
    public int superGaugeMax;
    public int iFramesDuration;
    public int staggerFrames;
    public float knockbackForce;

    // Estadísticas de combate (Player/NPC Script)
    public float moveSpeed;
    public float projectileSpeed;
    public float inputDelay;
    public float attackRadius;

    public Image[] enemySelectionBoxes = new Image[3];
    public GameObject[] enemyPrefabs = new GameObject[3];
    public Image[] selectionBoxes = new Image[3];
    public GameObject[] prefabs = new GameObject[3];

    void Start()
    {
        foreach (var img in this.selectionBoxes)
        {
            img.gameObject.SetActive(false);
        }
        SelectPlayer(0);
        foreach (var img in enemySelectionBoxes)
        {
            img.gameObject.SetActive(false);
        }
        SelectEnemy(0);

        // Para seleccionar a Goku como jugador
        SeleccionDePersonajes.Instance.SelectPlayer(1); // Asumiendo que Goku es índice 0

        // Para seleccionar a Toad como NPC
        SeleccionDePersonajes.Instance.SelectEnemy(1); // Asumiendo que Toad es índice 1
    }


    [System.Serializable]
    public class EstadisticasPersonaje
    {
        public string nombrePersonaje;
        public int vidaMaxima;
        public int superGaugeMax;
        public int iFramesDuration;
        public int staggerFrames;
        public float knockbackForce;
        public float moveSpeed;
        public float projectileSpeed;
        public int danioAtaqueLigero;
        public int danioAtaquePesado;
        public int danioAtaqueEspecial;
        public int costoSuper;
        public float attackRadius = 2f;    
        public float inputDelay = 1.5f;
    }

    public EstadisticasPersonaje[] personajes;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InicializarPersonajes();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InicializarPersonajes()
    {
        personajes = new EstadisticasPersonaje[3];

        // Estadisticas de Goku
        personajes[0] = new EstadisticasPersonaje
        {
            nombrePersonaje = "Goku",
            vidaMaxima = 100,
            superGaugeMax = 15,
            iFramesDuration = 30,
            staggerFrames = 20,
            knockbackForce = 5f,
            moveSpeed = 12f,
            projectileSpeed = 10f,
            danioAtaqueLigero = 2,
            danioAtaquePesado = 5,
            danioAtaqueEspecial = 6,
            costoSuper = 3
        };

        // Estadisticas de Toad
        personajes[1] = new EstadisticasPersonaje
        {
            nombrePersonaje = "Toad",
            vidaMaxima = 120,
            superGaugeMax = 12,
            iFramesDuration = 35,
            staggerFrames = 25,
            knockbackForce = 6f,
            moveSpeed = 10f,
            projectileSpeed = 8f,
            danioAtaqueLigero = 3,
            danioAtaquePesado = 7,
            danioAtaqueEspecial = 8,
            costoSuper = 4
        };

        // Estadisticas de Woddy
        personajes[2] = new EstadisticasPersonaje
        {
            nombrePersonaje = "Woddy",
            vidaMaxima = 80,
            superGaugeMax = 18,
            iFramesDuration = 25,
            staggerFrames = 15,
            knockbackForce = 4f,
            moveSpeed = 14f,
            projectileSpeed = 12f,
            danioAtaqueLigero = 2,
            danioAtaquePesado = 4,
            danioAtaqueEspecial = 5,
            costoSuper = 2
        };
    }

    public void AplicarConfiguracionPersonaje(int indicePersonaje, GameObject personaje, bool esJugador)
    {
        if (indicePersonaje < 0 || indicePersonaje >= personajes.Length)
            return;

        EstadisticasPersonaje estadisticas = personajes[indicePersonaje];

        // Obtener el EntityData del GameObject
        EntityData datosEntidad = personaje.GetComponent<EntityData>();
        if (datosEntidad != null)
        {
            datosEntidad.vidaMaxima = estadisticas.vidaMaxima;
            datosEntidad.vidaActual = estadisticas.vidaMaxima;
            datosEntidad.superGaugeMax = estadisticas.superGaugeMax;
            datosEntidad.iFramesDuration = estadisticas.iFramesDuration;
            datosEntidad.staggerFrames = estadisticas.staggerFrames;
            datosEntidad.knockbackForce = estadisticas.knockbackForce;
        }



        // Configurar Script según tipo de personaje
        if (esJugador)
        {
            PlayerScript scriptJugador = personaje.GetComponent<PlayerScript>();
            if (scriptJugador != null)
            {
                scriptJugador.moveSpeed = estadisticas.moveSpeed;
                scriptJugador.projectileSpeed = estadisticas.projectileSpeed;
                scriptJugador.inputDelay = estadisticas.inputDelay;
                scriptJugador.attackRadius = estadisticas.attackRadius;
            }
        }
        else
        {
            NPCScript scriptNPC = personaje.GetComponent<NPCScript>();
            if (scriptNPC != null)
            {
                scriptNPC.moveSpeed = estadisticas.moveSpeed;
                scriptNPC.projectileSpeed = estadisticas.projectileSpeed;
                scriptNPC.inputDelay = estadisticas.inputDelay;
                scriptNPC.attackRadius = estadisticas.attackRadius;
            }
        }
    }

    public void SelectPlayer(int index)
    {
        // Actualizar UI de selección
        foreach (var img in selectionBoxes)
        {
            img.gameObject.SetActive(false);
        }
        selectionBoxes[index].gameObject.SetActive(true);

        // Guardar prefab seleccionado
        PlayerStorage.playerPrefab = prefabs[index];

        // Aplicar configuración al prefab
        AplicarConfiguracionPersonaje(index, PlayerStorage.playerPrefab, true);
    }

    public void SelectEnemy(int index)
    {
        // Actualizar UI de selección
        foreach (var img in enemySelectionBoxes)
        {
            img.gameObject.SetActive(false);
        }
        enemySelectionBoxes[index].gameObject.SetActive(true);

        // Guardar prefab seleccionado
        EnemyStorage.enemyPrefab = enemyPrefabs[index];

        // Aplicar configuración al prefab
        AplicarConfiguracionPersonaje(index, EnemyStorage.enemyPrefab, false);
    }
}