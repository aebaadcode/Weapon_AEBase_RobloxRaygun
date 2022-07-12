datablock AudioProfile(RBLX_RaygunFireSound)
{
   filename    = "./Sounds/RBLX_RaygunFire.wav";
   description = LightClose3D;
   preload = true;
};

datablock AudioProfile(RBLX_RaygunLaserHit)
{
   filename    = "./Sounds/RBLX_RaygunLaserHit.wav";
   description = LightClose3D;
   preload = true;
};

// Taser
datablock DebrisData(RBLX_RaygunBatteryDebris)
{
	shapeFile = "./RBLXRaygun/RBLXRaygunBattery.dts";
	lifetime = 2.0;
	minSpinSpeed = -700.0;
	maxSpinSpeed = -600.0;
	elasticity = 0.5;
	friction = 0.1;
	numBounces = 3;
	staticOnMaxBounce = true;
	snapOnMaxBounce = false;
	fade = true;

	gravModifier = 2;
};

//STUFF

datablock ParticleData(RBLX_RaygunSparkleParticle)
{
	  gravityCoefficient   = 0.5;
	dragCoefficient		= 5;
	windCoefficient		= 0.0;
	constantAcceleration	= 0.0;
	lifetimeMS		= 1000;
	lifetimeVarianceMS	= 0;
	spinSpeed		= 0.0;
	spinRandomMin		= 0.0;
	spinRandomMax		= 0.0;
	useInvAlpha		= false;
	animateTexture		= false;
	textureName		= "./sparkle.png";

	//Interpolation variables
	colors[0]	= "0 1 0 1";
	colors[1]	= "0 1 0 1";
	colors[2]	= "0 1 0 1";
	sizes[0]	= 0.35;
	sizes[1]	= 0.35;
	sizes[2]	= 0.0;
	times[0]	= 0.0;
	times[1]	= 0.1;
	times[2]	= 1.0;
};

datablock ParticleEmitterData(RBLX_RaygunSparkleEmitter)
{
   ejectionPeriodMS = 50;
   periodVarianceMS = 1;
   ejectionVelocity = 1;
   ejectionOffset   = 0.4;
   velocityVariance = 1;
   thetaMin         = 0;
   thetaMax         = 30;
   phiReferenceVel  = 42000;
   phiVariance      = 0;
   overrideAdvance = false;
   particles = "RBLX_RaygunSparkleParticle";
   uiName = "XLS Mark II Sparkle";

};

datablock ExplosionData(RBLX_RaygunExplosion)
{
   //explosionShape = "";
   lifeTimeMS = 150;

   emitter[0] = RBLX_RaygunExplosionEmitter;
   particleDensity = 1000;
   particleRadius = 1.0;

   faceViewer     = true;
   explosionScale = "1 1 1";

   shakeCamera = true;
   camShakeFreq = "7.0 8.0 7.0";
   camShakeAmp = "1.0 1.0 1.0";
   camShakeDuration = 0.0;
   camShakeRadius = 0.0;

   // Dynamic light
   lightStartRadius = 5;
   lightEndRadius = 1;
   lightStartColor = "0 1 0.0";
   lightEndColor = "0 0 0";

   //impulse
   impulseRadius = 3.5;
   impulseForce = 500;

   //radius damage
   radiusDamage        = 1;
   damageRadius        = 0.5;
};


datablock ProjectileData(RBLX_RaygunProjectile : AETrailedProjectile)
{
   projectileShapeName = "./RBLXRaygun/RBLXRaygunProjectile.dts";
   
   explosion           = RBLX_RaygunExplosion;
    bloodExplosion      = RBLX_RaygunExplosion;
   particleEmitter     = RBLX_RaygunSparkleEmitter;


    lifetime            = 1000;
    fadeDelay           = 750;

   brickExplosionRadius = 0;
   brickExplosionImpact = false; //destroy a brick if we hit it directly?
   brickExplosionForce  = 0;
   brickExplosionMaxVolume = 0;
   brickExplosionMaxVolumeFloating = 0;

    hasLight    = true;
    lightRadius = 3.0;
    lightColor  = "0 1 0";

   gravityMod = 0;
};

function RBLX_RaygunProjectile::onCollision(%this, %obj, %col, %fade, %pos, %normal, %velocity)
{
	AETrailedProjectile::onCollision(%this, %obj, %col, %fade, %pos, %normal, %velocity);
}

function RBLX_RaygunProjectile::Damage(%this, %obj, %col, %fade, %pos, %normal)
{
	%col.zapTicks = 10; // 250ms ticks
	%col.zapDamage = 5; // damage per tick
	%col.zap();
	AETrailedProjectile::Damage(%this, %obj, %col, %fade, %pos, %normal);
}

//END OF STUFF


//////////
// item //
//////////
datablock ItemData(RBLX_RaygunItem)
{
	category = "Weapon";  // Mission editor category
	className = "Weapon"; // For inventory system

	 // Basic Item Properties
	shapeFile = "./RBLXRaygun/RBLXRaygun.dts";
	rotate = false;
	mass = 1;
	density = 0.2;
	elasticity = 0.2;
	friction = 0.6;
	emap = true;

	//gui stuff
	uiName = "RBLX: XLS Mark II";
	iconName = "./Icons/44";
	doColorShift = true;
	colorShiftColor = "0.55 0.5 0.5 1";

	 // Dynamic properties defined by the scripts
	image = RBLX_RaygunImage;
	canDrop = true;

	AEAmmo = 10;
	AEType = AE_MediumPAmmoItem.getID();
	AEBase = 1;

	RPM = 30;
	recoil = "No"; 
	uiColor = "1 1 1";
	description = "The XME brand made in America taser, loaded with mostly non lethal electric bolts." NL "This big fella is capable of stopping any fentanyl addicts dead in their tracks, guaranteed!";

	useImpactSounds = true;
	softImpactThreshold = 2;
	softImpactSound = "AEWepImpactSoft1Sound AEWepImpactSoft2Sound AEWepImpactSoft3Sound";
	hardImpactThreshold = 8;
	hardImpactSound = "AEWepImpactHard1Sound AEWepImpactHard2Sound AEWepImpactHard3Sound";
};

////////////////
//weapon image//
////////////////
datablock ShapeBaseImageData(RBLX_RaygunImage)
{
   // Basic Item properties
	shapeFile = "./RBLXRaygun/RBLXRaygun.dts";
   emap = true;

   // Specify mount point & offset for 3rd person, and eye offset
   // for first person rendering.
   mountPoint = 0;
   offset = "-0.02 0.025 -0.035";
   eyeOffset = "0 0 0";
   rotation = eulerToMatrix( "0 0 0" );

   // When firing from a point offset from the eye, muzzle correction
   // will adjust the muzzle vector to point to the eye LOS point.
   // Since this weapon doesn't actually fire from the muzzle point,
   // we need to turn this off.  
   correctMuzzleVector = true;

   // Add the WeaponImage namespace as a parent, WeaponImage namespace
   // provides some hooks into the inventory system.
   className = "WeaponImage";

   // Projectile && Ammo.
   item = RBLX_RaygunItem;
   ammo = " ";
   projectile = RBLX_RaygunProjectile;
   projectileType = Projectile;

   Mag = a;
   shellExitDir        = "-1 0 0.5";
   shellExitOffset     = "0 0 0";
   shellExitVariance   = 25;	
   shellVelocity       = 5.0;
	
   //melee particles shoot from eye node for consistancy
	melee = false;
   //raise your arm up or not
	armReady = true;
	hideHands = false;
	safetyImage = RBLX_RaygunSafetyImage;
	doColorShift = true;
	colorShiftColor = RBLX_RaygunItem.colorShiftColor;//"0.400 0.196 0 1.000";

	bulletScale = "1 1 1";

	projectileDamage = 50;
	projectileCount = 1;
	projectileHeadshotMult = 2;
	projectileVelocity = 150;
	projectileTagStrength = 1;  // tagging strength
	projectileTagRecovery = 0.01; // tagging decay rate
	projectileTagIgnore = true; // ignore global tag multipliers
    alwaysSpawnProjectile = true;
	recoilHeight = 0;
	recoilWidth = 0;
	recoilWidthMax = 0;
	recoilHeightMax = 20;

	spreadBurst = 1; // how much shots it takes to trigger spread i think
	spreadReset = 0; // m
	spreadBase = 0;
	spreadMin = 0;
	spreadMax = 0;
	
	projectileFalloffStart = $ae_falloffPistolStart;
	projectileFalloffEnd = $ae_falloffPistolEnd;
	projectileFalloffDamage = $ae_falloffPistol;

   //Mag = " ";

   // Images have a state system which controls how the animations
   // are run, which sounds are played, script callbacks, etc. This
   // state system is downloaded to the client so that clients can
   // predict state changes and animate accordingly.  The following
   // system supports basic ready->fire->reload transitions as
   // well as a no-ammo->dryfire idle state.
   // Initial start up state
	
	stateName[0]                     	= "Activate";
	stateTimeoutValue[0]             	= 0.01;
	stateTransitionOnTimeout[0]       	= "LoadCheckA";
	stateSequence[0]			= "root";

	stateName[1]                     	= "Ready";
	stateScript[1]				= "onReady";
	stateTransitionOnNotLoaded[1]     = "Empty";
	stateTransitionOnNoAmmo[1]       	= "ReloadAlt";
	stateTransitionOnTriggerDown[1]  	= "preFire";
	stateAllowImageChange[1]         	= true;

	stateName[2]                       = "preFire";
	stateTransitionOnTimeout[2]        = "Fire";
	stateTransitionOnNoAmmo[2]       	= "FireEmpty";
	stateScript[2]                     = "AEOnFire";
	stateFire[2]                       = true;
	stateEjectShell[2]                       = true;

	stateName[3]                    = "Fire";
	stateTransitionOnTimeout[3]     = "FireLoadCheckA";
	stateAllowImageChange[3]        = false;
	stateSequence[3]                = "Fire";
	stateWaitForTimeout[3]			= true;
	
	stateName[4]                    = "FireEmpty";
	stateTransitionOnTimeout[4]     = "FireLoadCheckA";
	stateAllowImageChange[4]        = false;
	stateSequence[4]                = "FireEmpty";
	stateWaitForTimeout[4]			= true;
	
	stateName[5]				= "LoadCheckA";
	stateScript[5]				= "AEMagLoadCheck";
	stateTimeoutValue[5]			= 0.1;
	stateTransitionOnTimeout[5]		= "LoadCheckB";
	
	stateName[6]				= "LoadCheckB";
	stateTransitionOnAmmo[6]		= "Ready";
	stateTransitionOnNotLoaded[6] = "Empty";
	stateTransitionOnNoAmmo[6]		= "Reload2Wait";

	stateName[7]				= "Reload";
	stateTimeoutValue[7]			= 0.25;
	stateScript[7]				= "onReloadStart";
	stateTransitionOnTimeout[7]		= "ReloadMagOut";
	stateWaitForTimeout[7]			= true;
	stateSequence[7]			= "ReloadStart";
	
	stateName[8]				= "ReloadAlt";
	stateTimeoutValue[8]			= 0.35;
	stateScript[8]				= "onReloadStartAlt";
	stateTransitionOnTimeout[8]		= "ReloadMagOut";
	stateWaitForTimeout[8]			= true;
	stateSequence[8]			= "ReloadStartAlt";
	
	stateName[9]				= "ReloadMagOut";
	stateTimeoutValue[9]			= 0.2;
	stateTransitionOnTimeout[9]		= "ReloadMagIn";
	stateSequence[9]			= "MagOut";
	
	stateName[10]				= "ReloadMagIn";
	stateTimeoutValue[10]			= 0.35;
	stateScript[10]				= "onReloadMagIn";
	stateTransitionOnTimeout[10]		= "ReloadEnd";
	stateWaitForTimeout[10]			= true;
	stateSequence[10]			= "MagIn";
	
	stateName[11]				= "ReloadEnd";
	stateTimeoutValue[11]			= 0.4;
	stateScript[11]				= "onReloadEnd";
	stateTransitionOnTimeout[11]		= "Reloaded";
	stateWaitForTimeout[11]			= true;
	stateSequence[11]			= "ReloadEnd";
	
	stateName[12]				= "FireLoadCheckA";
	stateScript[12]				= "AEMagLoadCheck";
	stateTimeoutValue[12]			= 0.085;
	stateTransitionOnTriggerUp[12]		= "FireLoadCheckB";
	
	stateName[13]				= "FireLoadCheckB";
	stateTransitionOnAmmo[13]		= "Ready";
	stateTransitionOnNoAmmo[13]		= "Reload2Wait";	
	stateTransitionOnNotLoaded[13]  = "Ready";
		
	stateName[14]				= "Reloaded";
	stateTimeoutValue[14]			= 0.1;
	stateScript[14]				= "AEMagReloadAll";
	stateTransitionOnTimeout[14]		= "Ready";

	stateName[15]				= "Reload2Wait";
	stateTimeoutValue[15]			= 0.25;
	stateTransitionOnTimeout[15]		= "Reload";

	stateName[16]          = "Empty";
	stateTransitionOnTriggerDown[16]  = "Dryfire";
	stateTransitionOnLoaded[16] = "Reload2Wait";
	stateScript[16]        = "AEOnEmpty";

	stateName[17]           = "Dryfire";
	stateTransitionOnTriggerUp[17] = "Empty";
	stateWaitForTimeout[17]    = false;
	stateScript[17]      = "onDryFire";
};

// THERE ARE THREE STAGES OF VISUAL RECOIL, NONE, PLANT, JUMP

function RBLX_RaygunImage::AEOnFire(%this,%obj,%slot)
{	
	%obj.stopAudio(0); 
    %obj.playAudio(0, RBLX_RaygunFireSound);
  
	%obj.blockImageDismount = true;
	%obj.schedule(250, unBlockImageDismount);

	if (%obj.aeAmmo[%obj.currTool, %this.aeUseAmmo, %slot] == 1)
	{
        serverPlay3D(RBLX_RaygunEject, %obj.getHackPosition());
        %obj.schedule(75, playAudio, 1, RBLX_RaygunOpenHatch);
        %obj.reload3Schedule = %this.schedule(100,onMagDrop,%obj,%slot);
        %obj.reload4Schedule = schedule(getRandom(700,800),0,serverPlay3D,AEMagPlasticAr @ getRandom(1,3) @ Sound,%obj.getPosition());
	    %obj.aeplayThread(2, plant);		
	}
	
	Parent::AEOnFire(%this, %obj, %slot);
}

function RBLX_RaygunImage::onDryFire(%this, %obj, %slot)
{
	%obj.aeplayThread(2, plant);
	serverPlay3D(AEDryFireSound, %obj.getHackPosition());
}

function RBLX_RaygunImage::onReloadMagOut(%this,%obj,%slot)
{
  %obj.aeplayThread(2, shiftleft);
  %obj.aeplayThread(3, plant);
}

function RBLX_RaygunImage::onReloadMagIn(%this,%obj,%slot)
{
    %obj.schedule(75, playAudio, 1, RBLX_RaygunInsert);
    %obj.schedule(75, "aeplayThread", "3", "shiftright");
    %obj.schedule(75, "aeplayThread", "2", "shiftleft");
}

function RBLX_RaygunImage::onReloadEnd(%this,%obj,%slot)
{
  %obj.aeplayThread(2, shiftleft);
  %obj.schedule(150, playAudio, 1, RBLX_RaygunCloseHatch);
//    %obj.schedule(350, playAudio, 1, RBLX_RaygunEquip);
}

// MAGAZINE DROPPING

function RBLX_RaygunImage::onReloadStart(%this,%obj,%slot)
{
  %obj.aeplayThread(2, plant);
}

function RBLX_RaygunImage::onReloadStartAlt(%this,%obj,%slot)
{
  serverPlay3D(RBLX_RaygunEject, %obj.getHackPosition());
  %obj.schedule(75, playAudio, 1, RBLX_RaygunOpenHatch);
  %obj.reload3Schedule = %this.schedule(75,onMagDrop,%obj,%slot);
  %obj.reload4Schedule = schedule(getRandom(700,800),0,serverPlay3D,AEMagPlasticAr @ getRandom(1,3) @ Sound,%obj.getPosition());
  %obj.aeplayThread(2, plant);
}

function RBLX_RaygunImage::onReady(%this,%obj,%slot)
{
	%obj.baadDisplayAmmo(%this);

	if(getSimTime() - %obj.reloadTime[%this.getID()] <= %this.stateTimeoutValue[0] * 1000 + 1000)
		%obj.schedule(0, setImageAmmo, %slot, 0);
}

// HIDES ALL HAND NODES

function RBLX_RaygunImage::onMount(%this,%obj,%slot)
{
	%obj.stopAudio(1); 
    %obj.playAudio(1, RBLX_RaygunEquip);
	%this.AEMountSetup(%obj, %slot);
	parent::onMount(%this,%obj,%slot);
}

// APLLY BODY PARTS IS LIKE PRESSING CTRL O AND ESC, IT APPLIES THE AVATAR COLORS FOR YOU

function RBLX_RaygunImage::onUnMount(%this,%obj,%slot)
{
	%this.AEUnmountCleanup(%obj, %slot);

	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);

	parent::onUnMount(%this,%obj,%slot);	
}

/////////////////////////////////////////////////////////////////////
///////////////////////// MAG DROP FUNCTIONS/////////////////////////
/////////////////////////////////////////////////////////////////////

function RBLX_RaygunImage::onMagDrop(%this,%obj,%slot)
{
	%a = new Camera()
	{
		datablock = Observer;
		position = %obj.getPosition();
		scale = "1 1 1";
	};

	%a.setTransform(%obj.getSlotTransform(0));
	%a.mountImage(RBLX_RaygunMagImage,0);
	%a.schedule(2500,delete);
}

//////////////////////////////////////////////////////////////////
///////////////////////// MAG DROP IMAGES/////////////////////////
//////////////////////////////////////////////////////////////////

datablock ShapeBaseImageData(RBLX_RaygunMagImage)
{
	shapeFile = "base/data/shapes/empty.dts";
	mountPoint = 0;
	offset = "0 -0.1 0.4";
   rotation = eulerToMatrix( "0 0 0" );	
	
	casing = RBLX_RaygunBatteryDebris;
	shellExitDir        = "-1 0 0.5";
	shellExitOffset     = "0 0 0";
	shellExitVariance   = 10.0;	
	shellVelocity       = 4.0;
	
	stateName[0]					= "Ready";
	stateTimeoutValue[0]			= 0.01;
	stateTransitionOnTimeout[0] 	= "EjectA";
	
	stateName[1]					= "EjectA";
	stateEjectShell[1]				= true;
	stateTimeoutValue[1]			= 1;
	stateTransitionOnTimeout[1] 	= "Done";
	
	stateName[2]					= "Done";
	stateScript[2]					= "onDone";
};

function RBLX_RaygunMagImage::onDone(%this,%obj,%slot)
{
	%obj.unMountImage(%slot);
}

////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////
////////////////////////////////////////////////////////////

datablock ShapeBaseImageData(RBLX_RaygunSafetyImage)
{
	shapeFile = "./RBLXRaygun/RBLXRaygun.dts";
   emap = true;
   mountPoint = 0;
   offset = "0 0 0";
   eyeOffset = "0 0 0";
   rotation = eulerToMatrix( "0 0 0" );
   correctMuzzleVector = true;
   className = "WeaponImage";
   item = RBLX_RaygunItem;
   ammo = " ";
   melee = false;
   armReady = false;
   hideHands = false;
   safetyImage = RBLX_RaygunImage;
   doColorShift = true;
   colorShiftColor = RBLX_RaygunItem.colorShiftColor;

	isSafetyImage = true;

	stateName[0]                    	= "Activate";
	stateTimeoutValue[0]            	= 0.1;
	stateTransitionOnTimeout[0]     	= "Ready";
	
	stateName[1]                     	= "Ready";
	stateTransitionOnTriggerDown[1]  	= "Done";
	
	stateName[2]				= "Done";
	stateScript[2]				= "onDone";

};

function RBLX_RaygunSafetyImage::onDone(%this,%obj,%slot)
{
	%obj.mountImage(%this.safetyImage, 0);
}

function RBLX_RaygunSafetyImage::onMount(%this,%obj,%slot)
{
	%this.AEMountSetup(%obj, %slot);
	%obj.aeplayThread(1, root);
	cancel(%obj.reload3Schedule);
	cancel(%obj.reload4Schedule);
	parent::onMount(%this,%obj,%slot);
}

function RBLX_RaygunSafetyImage::onUnMount(%this, %obj, %slot)
{
	%this.AEUnmountCleanup(%obj, %slot);
	%obj.aeplayThread(1, armReadyRight);	
	parent::onUnMount(%this,%obj,%slot);	
}