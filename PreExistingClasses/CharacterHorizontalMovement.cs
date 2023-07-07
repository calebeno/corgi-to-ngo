
// Snippet from CharacterHorizontalMovement.cs

public override void ProcessAbility()
{
    if (!IsOwner) return;  // <==== this added
    base.ProcessAbility();

    HandleHorizontalMovement();
    DetectWalls(true);
}

/// <summary>
/// Called at the very start of the ability's cycle, and intended to be overridden, looks for input and calls
/// methods if conditions are met
/// </summary>
protected override void HandleInput()
{
    if (!IsOwner) return;  // <==== this added
    if (!ReadInput)
    {
        return;
    }



// Rest of file....
