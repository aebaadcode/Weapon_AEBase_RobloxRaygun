//we need the base add-on for this, so force it to load
%error = ForceRequiredAddOn("Weapon_AEBase");

if(%error == $Error::AddOn_NotFound)
{
	//we don't have the base, so we're screwed =(
	error("ERROR: AEBase_RobloxRaygun - required add-on Weapon_AEBase not found");
}
else
{
	exec("./Sounds/Sounds.cs");
	exec("./Effect_StunZap.cs");

	exec("./Weapon_RBLXRaygun.cs");
}