using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Player : MovingObject
{
    public int wallDamage = 1;
    public int pointsPerFood = 10;
    public int pointsPerBattery = 10;
    public int pointsPerSoda = 20;
    public float restartLevelDelay = 1f;
    public int clickCount = 0;

    public Text foodText;
    public Text BatteryText;
    public AudioClip moveSound1;
    public AudioClip moveSound2;
    public AudioClip eatSound1;
    public AudioClip eatSound2;
    public AudioClip drinkSound1;
    public AudioClip drinkSound2;
    public AudioClip gameOverSound;

    public Slider FoodSlider;
    public Slider BatterSlider;

    private GameObject chat1;
    private GameObject chat2;
    private GameObject chat3;
    //private GameObject chat4;
    //private GameObject chat5;
    //private GameObject chat6;

    private GameObject flashlight;
    private Quaternion rotation;

    private Animator animator;
    private int food;
    private int battery;
    private float lightScaleX = 4.5f, lightScaleY = 7f, lightScaleZ = 7f; // 5.5 8 8

    protected override void Start()
    {
        animator = GetComponent<Animator>();
        flashlight = GameObject.Find("Light");
        flashlight.SetActive(true);
        flashlight.transform.localScale = new Vector3(lightScaleX, lightScaleY, lightScaleZ);

        food = GameManager.instance.playerFoodPoints;
        battery = GameManager.instance.playerBatteryPoints;

        FoodSlider.value = food;
        BatterSlider.value = battery;

        chat1 = GameObject.Find("Chat1");
        chat1.SetActive(false);
        chat2 = GameObject.Find("Chat2");
        chat2.SetActive(false);
        chat3 = GameObject.Find("Chat3");
        chat3.SetActive(false);
        //chat4 = GameObject.Find("Chat4");
        //chat4.SetActive(false);
        //chat5 = GameObject.Find("Chat5");
        //chat5.SetActive(false);
        //chat6 = GameObject.Find("Chat6");
        //chat6.SetActive(false);

        foodText.text = "Food: " + food;
        BatteryText.text = "Battery: " + battery;

        base.Start();
    }

    private void OnDisable()
    {
        GameManager.instance.playerFoodPoints = food;
        GameManager.instance.playerBatteryPoints = battery;
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.playersTurn) return;

        int horizontal = 0;
        int vertical = 0;

        horizontal = (int)Input.GetAxisRaw("Horizontal"); // 가로
        vertical = (int)Input.GetAxisRaw("Vertical"); // 세로

        transform.Translate(Vector3.right * Time.deltaTime * horizontal, Space.World);
        transform.Translate(Vector3.up * Time.deltaTime * vertical, Space.World);
        // 테스트를 위한 키보드 이동 끝


        // 오브젝트에 따른 HP Bar 위치 이동
        flashlight.transform.position = Camera.main.WorldToScreenPoint(transform.position);
        chat1.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0.3f, 1f, 0));
        chat2.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0.3f, 1f, 0));
        chat3.transform.position = Camera.main.WorldToScreenPoint(transform.position + new Vector3(0.3f, 1f, 0));


        if (horizontal != 0)
            vertical = 0;

        if (horizontal != 0 || vertical != 0)
            AttempMove<Wall>(horizontal, vertical);
    }

    protected override void AttempMove<T>(int xDir, int yDir)
    {
        if (xDir == 1f)
        {
            rotation = Quaternion.Euler(0, 0, 90);
            flashlight.transform.rotation = rotation;
        }
        else if (xDir == -1f)
        {
            rotation = Quaternion.Euler(0, 0, -90);
            flashlight.transform.rotation = rotation;
        }
        else if(yDir == 1f)
        {
            rotation = Quaternion.Euler(0, 0, 180);
            flashlight.transform.rotation = rotation;
        }
        else if(yDir == -1f)
        {
            rotation = Quaternion.Euler(0, 0, 0);
            flashlight.transform.rotation = rotation;
        }
        clickCount++;
        food--;
        battery--;
        lightScaleX -= 0.05f;
        if (lightScaleX < 1f)
            lightScaleX = 1f;
        lightScaleY -= 0.05f;
        if (lightScaleY < 2f)
            lightScaleY = 2f;
        lightScaleZ -= 0.05f;
        if (lightScaleZ < 2f)
            lightScaleZ = 2f;
        flashlight.transform.localScale = new Vector3(lightScaleX, lightScaleY, lightScaleZ);

        FoodSlider.value = food;
        BatterSlider.value = battery;

        if (clickCount % 5 == 0)
        {
            chat1.SetActive(true);
        }
        else if (clickCount % 7 == 0)
        {
            chat2.SetActive(true);
        }
        else if (clickCount % 11 == 0)
        {
            chat3.SetActive(true);
        }
        else
        {
            chat1.SetActive(false);
            chat2.SetActive(false);
            chat3.SetActive(false);
        }

        foodText.text = "Food: " + food;
        BatteryText.text = "Battery: " + battery;

        base.AttempMove<T>(xDir, yDir);

        RaycastHit2D hit;
        if (Move(xDir, yDir, out hit))
        {
            SoundManager.instance.RandomizeSfx(moveSound1, moveSound2);
        }


        //CheckIfGameOver();

        GameManager.instance.playersTurn = false;

    }

    public void Collision(Collider2D other)
    {
        if (other.tag == "Exit")
        {
            Invoke("Restart", restartLevelDelay);
            enabled = false;
        }
        else if (other.tag == "Food")
        {
            food += pointsPerFood;
            if (food > 200)
                food = 200;
            FoodSlider.value = food;
            foodText.text = "+" + pointsPerFood + " Food: " + food;
            SoundManager.instance.RandomizeSfx(eatSound1, eatSound2);
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "Soda")
        {
            food += pointsPerSoda;
            if (food > 200)
                food = 200;
            FoodSlider.value = food;
            foodText.text = "+" + pointsPerSoda + " Food: " + food;
            SoundManager.instance.RandomizeSfx(drinkSound1, drinkSound2);
            other.gameObject.SetActive(false);
        }
        else if (other.tag == "Battery")
        {
            battery += pointsPerBattery;
            if (battery > 200)
                battery = 200;
            BatterSlider.value = battery;
            BatteryText.text = "+" + pointsPerBattery + " Battery: " + battery;
            lightScaleX += 0.15f;
            lightScaleY += 0.15f;
            lightScaleZ += 0.15f;
            flashlight.transform.localScale = new Vector3(lightScaleX, lightScaleY, lightScaleZ);
            //foodText.text = "+" + pointsPerSoda + " Food: " + food;
            SoundManager.instance.RandomizeSfx(drinkSound1, drinkSound2);
            other.gameObject.SetActive(false);
        }
    }

    protected override void OnCantMove<T>(T component)
    {
        Wall hitWall = component as Wall;
        hitWall.DamageWall(wallDamage);
        animator.SetTrigger("playerChop");
    }

    private void Restart()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    public void LoseFood(int loss)
    {
        animator.SetTrigger("playerHit");
        food -= loss;
       // battery -= loss;

        FoodSlider.value -= loss;

        foodText.text = "-" + loss + " Food: " + food;
        //CheckIfGameOver();
    }

    //private void CheckIfGameOver()
    //{
    //    if (food <= 0 || battery <= 0)
    //    {
    //        SoundManager.instance.PlaySingle(gameOverSound);
    //        SoundManager.instance.musicSource.Stop();
    //        GameManager.instance.GameOver();
    //    }
    //}
}
