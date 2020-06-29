public static class PlayerStats
{
    static int phase = 1;
    static int score;
    static int coins;

    public static int Phase{
        get{
            return phase;
        }
        set{
            phase = value;
        }
    }

    public static int Score{
        get{
            return score;
        }
        set{
            score = value;
        }
    }

    public static int Coins{
        get{
            return coins;
        }
        set{
            coins = value;
        }
    }
}
