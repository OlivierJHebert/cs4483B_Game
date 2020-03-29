public interface IMove
{
    void TriggerWaterEffect(float time);//triggers slows and other water effects
    void knockback(bool right, bool damaged);//triggers "knockback" arc and animations when hit
}
