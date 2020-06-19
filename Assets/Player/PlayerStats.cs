public static class PlayerStats
{
    public static int phase = 1;
    public static int score;
    public static int coins;

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
