using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprite_animator : MonoBehaviour
{
    private SpriteRenderer self;
    public Sprite[] sprites;
    public float period;
    public int RepeatTimes; ///-1 for infinity
    [SerializeField] private bool infinite;
    [SerializeField] private float timer = 0;
    [SerializeField] private float step;
    [SerializeField] private float next_mark;
    [SerializeField] private int current = 0;

    private void Start()
    {
        infinite = false;
        self = GetComponent<SpriteRenderer>();
        step = period / sprites.Length;
        next_mark = step;
        self.sprite = sprites[current];
        current = 0;
        timer = 0;
        if (self == null) GetComponent<Sprite_animator>().enabled = false;
        if (period == 0) GetComponent<Sprite_animator>().enabled = false;
        if (RepeatTimes == -1) infinite = true;
        else if (RepeatTimes < 0) GetComponent<Sprite_animator>().enabled = false;
    }
    public void init(Sprite[] _sprites, int rpt) 
    {
        sprites = _sprites;
        RepeatTimes = rpt;
        Start();
    }
    private void Update()
    {
        if (sprites.Length != 0)
        {
            timer += Time.deltaTime;
            if (timer >= next_mark)
            {
                if (current != sprites.Length - 1)
                {
                    next_mark += step;
                    current++;
                    self.sprite = sprites[current];
                }
                else
                {
                    next_mark = step;
                    current = 0;
                    timer = 0f;
                    self.sprite = sprites[current];
                    if (!infinite) RepeatTimes -= 1;
                    if (RepeatTimes == 0)
                    {
                        self.sprite = sprites[current];
                        GetComponent<Sprite_animator>().enabled = false;
                    }
                }
            }
        }
    }
}
