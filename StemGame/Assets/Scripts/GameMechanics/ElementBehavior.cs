using UnityEngine;
using System.Collections;

public class ElementBehavior : MonoBehaviour {
	public string elementName;
    public Collider2D solidCollider;
	public ElementBehavior[] legalCombination;
	public ElementBehavior[] newCompound;
    public Vector3 desiredLocalScale = Vector3.zero;
	GrabbedBehavior grabbedBehavior;
    Transform frozenPlayer;

    public AudioSource audioS;
	//private bool isGrabbed;

	//Added in Nick P's code, updated temperature logic. Should change element states based on global temp variable. -Nick S

	TempManager tempManager = null;
	
	public enum State {SOLID = 0, LIQUID = 1, GAS = 2};
	protected bool sublime;
	State curState;
	Animator anim;
	public float meltingPoint;
	public float boilingPoint;

    public float activationTemp;
	
	float curTemp;

	//generic constructor
	public ElementBehavior(){
		elementName = "generic";
		curState = State.SOLID;
		meltingPoint = 1f;
		boilingPoint = 100f;
        activationTemp = 0.0f;
		curTemp = 0f;
		sublime = false;
	}

	//parametered constructor
	public ElementBehavior(string elementName, State curState, float freezingPoint, float meltingPoint, float boilingPoint, float curTemp, bool sublime, float activationTemp){
		this.elementName = elementName;
        this.activationTemp = activationTemp;
		this.curState = curState;
		this.meltingPoint = meltingPoint;
		this.boilingPoint = boilingPoint;
		this.curTemp = curTemp;
		this.sublime = sublime;
	}

	void Start(){
		TempManager[] tempManagers = FindObjectsOfType(typeof(TempManager)) as TempManager[];
		grabbedBehavior = GetComponent<GrabbedBehavior> ();
		if (tempManagers.Length != 0){
			tempManager = tempManagers[0];
		}
		anim = gameObject.GetComponent<Animator> ();
        audioS = gameObject.GetComponent<AudioSource>();
        if (desiredLocalScale.magnitude <= .001)
        {
            desiredLocalScale = transform.localScale;
        }
       // print(desiredLocalScale);
    }

	//Fixed update. Updates temp if it exists, changes states of element.
	void FixedUpdate(){
		if (tempManager != null) {
			curTemp = tempManager.getTemp();
		}
		updateState ();
	}

	//state machine
    /// <summary>
    /// updates the state of the element based on the temperature of the map
    /// </summary>
    /// <returns></returns>
	public State updateState(){
		if (curTemp < meltingPoint && curState != State.SOLID) {
			this.curState = State.SOLID;
			Debug.Log (elementName + " froze!");
			solidCollider.enabled = true;
			anim.SetInteger("state", 0);
            shiftPlayer();
            audioS.clip = Resources.Load("SFX/StemGameFreeze") as AudioClip;
            audioS.Play();
        } else if (!sublime && curTemp >= meltingPoint && curTemp < boilingPoint && curState != State.LIQUID) {
			this.curState = State.LIQUID;
			Debug.Log (elementName + " melted!");
			solidCollider.enabled = false;
            setPlayerFrozen(false);
			anim.SetInteger("state", 1);
            audioS.clip = Resources.Load("SFX/StemGameMelt") as AudioClip;
            audioS.Play();
        } else if (curTemp >= boilingPoint && curState != State.GAS) {
			this.curState = State.GAS;
			Debug.Log (elementName + " evaporated!");
			solidCollider.enabled = false;
            setPlayerFrozen(false);
			anim.SetInteger("state", 2);
            audioS.clip = Resources.Load("SFX/StemGameMelt") as AudioClip;
            audioS.Play();
        } else if (curTemp < boilingPoint && curState == State.GAS) {
			this.curState = State.LIQUID;
			Debug.Log (elementName + " condensated!");
			solidCollider.enabled = false;
            setPlayerFrozen(false);
			anim.SetInteger("state", 1);
            audioS.clip = Resources.Load("SFX/StemGameMelt") as AudioClip;
            audioS.Play();
        }
		return this.curState;
	}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="collider"></param>
	void OnTriggerEnter2D(Collider2D collider) {

		if (grabbedBehavior.getIsGrabbed()) {//This ensures that only one resulting element is produced when two blocks are combined -Nick S
			ElementBehavior collideElement = collider.GetComponent<ElementBehavior> ();
			if (collideElement != null) {
				checkLegalCombination (collideElement);

			}
		}
	}

    /// <summary>
    /// 
    /// </summary>
    void shiftPlayer()
    {
        GameObject player = GameObject.FindWithTag("Player");
        Vector3 checkDistacne = transform.position - player.transform.position;
        if (Mathf.Abs(checkDistacne.x) < .5f && Mathf.Abs(checkDistacne.y) < .5f && !player.GetComponent<WalkMechanics>().isFrozen)
        {
            bool checkSetPlayerFrozen = frozenPlayer == null;
            frozenPlayer = player.transform;
            player.GetComponent<PlayerController>().enabled = false;
            if(checkSetPlayerFrozen) 
              setPlayerFrozen(true);
        }

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isFrozen"></param>
    public void setPlayerFrozen(bool isFrozen)
    {
        if (isFrozen && frozenPlayer != null)
        {
            frozenPlayer.GetComponentInChildren<SpriteRenderer>().color = new Color (0, 30, 255, 255);
            frozenPlayer.GetComponent<PlayerController>().enabled = false;
            frozenPlayer.GetComponent<Collider2D>().enabled = false;
            frozenPlayer.position = this.transform.position;
        } else if (frozenPlayer != null)
        {
            frozenPlayer.GetComponent<Collider2D>().enabled = true;
            frozenPlayer.GetComponentInChildren<SpriteRenderer>().color = new Color(255, 255, 255, 255);
            frozenPlayer.GetComponent<PlayerController>().enabled = true;
            frozenPlayer = null;
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="checkBehavior"></param>
	void checkLegalCombination(ElementBehavior checkBehavior) {
		int i = 0;
		if (!grabbedBehavior.getIsGrabbed()) {
			return;
		}
		foreach (ElementBehavior ele in legalCombination) {

			if (curTemp >= newCompound[i].gameObject.GetComponent<ElementBehavior>().activationTemp &&checkBehavior.elementName == ele.elementName) {
				Destroy(checkBehavior.gameObject);
                GameObject temp;
				Instantiate(temp = newCompound[i].gameObject, checkBehavior.transform.position, new Quaternion());
                //temp.transform.localScale = checkBehavior.desiredLocalScale;
                temp.transform.localScale = new Vector3(.9f, .9f, 1);
                Destroy (this.gameObject);
                Instantiate(Resources.Load("ReactionSFX") as GameObject);
			}
			i++;
		}
	}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isGrabbed"></param>
	public void setIsGrabbed(bool isGrabbed) {
		//this.isGrabbed = isGrabbed;
	}

	//getters
	
	public string getName(){
		return elementName;
	}
	
	public State getCurState(){
		return curState;
	}
	
	public float getMeltingPoint(){
		return meltingPoint;
	}
	
	public float getBoilingPoint(){
		return boilingPoint;
	}

    /// <summary>
    /// Retrieves the current tempertature of teh 
    /// </summary>
    /// <returns></returns>
    public float getCurTemp()
    {
        return curTemp;
    }
	
	
	//setters
	
    /// <summary>
    /// Sets the name of the element
    /// </summary>
    /// <param name="elementName"></param>
    /// <returns></returns>
	public string setName(string elementName){
		this.elementName = elementName;
		return this.elementName;
	}
	
    /// <summary>
    /// Sets the current State of the element
    /// </summary>
    /// <param name="curState"></param>
    /// <returns></returns>
	public State setCurState(State curState){
		this.curState = curState;
		return this.curState;
	}
	
    /// <summary>
    /// Sets the melting point of the element
    /// </summary>
    /// <param name="meltingPoint"></param>
    /// <returns></returns>
	public float setMeltingPoint(float meltingPoint){
		this.meltingPoint = meltingPoint;
		return this.meltingPoint;
	}
	
    /// <summary>
    /// Set the boiling point of the element
    /// </summary>
    /// <param name="boilingPoint"></param>
    /// <returns></returns>
	public float setBoilingPoint(float boilingPoint){
		this.boilingPoint = boilingPoint;
		return this.boilingPoint;
	}
	
    /// <summary>
    /// Sets the current temperature for the element
    /// </summary>
    /// <param name="curTemp"></param>
    /// <returns></returns>
	public float setCurTemp(float curTemp){
		this.curTemp = curTemp;
		return this.curTemp;
	}

}
