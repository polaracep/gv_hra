namespace TBoGV;
internal interface IRecieveDmg
{
	int Hp { get; set; }
	void RecieveDmg(int damage);
}