namespace TBoGV;
internal interface IRecieveDmg
{
	int Hp { get; set; }
	int MaxHp { get; set; }
	void RecieveDmg(int damage);
}