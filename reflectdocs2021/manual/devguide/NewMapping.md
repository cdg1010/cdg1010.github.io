# Adding a new input mapping

Sometimes users will need to change or add new Input Mappings. In this document, we will show how we add a new mapping. The viewer uses Unity’s new Input System.

## Adding a Quick Pan Mapping

It was requested that the middle mouse button in the viewer be mapped to a PAN action.

### Opening the Input Action Editor

Go to Assets/Settings and double-click on the UnityEditorMapping:

![Input](../images/1.3/Input1.png)

This opens the Input Action Editor.

![Input](../images/1.3/Input2.png)


### Adding a new Quick Pan Action

We see the similar Quick Zoom Action which is mapped to the scroll wheel. We will add a new Action by pressing **+** next to the Actions header.

Rename it to **Quick Pan Action**.

Select the <No Binding> Right-Click and delete binding.

On the right hand side, in the Action Type dropdown, select Value”, and Control Type “Vector 2”, then click the “+” next to the Quick Pan Action header, this will let you choose a new “Mouse Drag Composite”.

Now, configure the Mouse Drag:

    Select Mouse/Middle Button
    Select Axis 1/Delta/X
    Select Axis 2/Delta/Y

We also need to scale the Delta/X/Y values. Click on the Mouse Drag line, and press on “+” next to Processors on the right-hand side. Pick “Scale”. Enter 0.003 for both X, Y values.

Your entry should look like this:

![Input](../images/1.3/Input3.png)

>> **Note:** Don't forget to press **Save Asset**.  

### Coding the New Action Callback

Now you need to add the code for this new action:

Open UINavigationController.cs, and go to the Awake() method.

![Input](../images/1.3/Input4.png)

You will need to add this line:

m_InputActionAsset["Quick Pan Action"].performed += OnQuickPan;

And implement the OnQuickPan() callback. You can look at the OnQuickZoom() method as an example.

```
private void OnQuickPan(InputAction.CallbackContext context)
{
        StopCoroutine("DelayEndQuickTool" );
        StartCoroutine("DelayEndQuickTool", new QuickToolData()
{
delay = k_ToolDebounceTime,
toolType = ToolType.PanTool
});

     var delta = context.ReadValue<Vector2>();
     Pan(delta);
}
```

### Compile and Run

Compile and Run and try your new Action Mapping. You should see the Toolbar on the right look like this, and be able to PAN when you press the middle mouse button and move the mouse around.

![Input](../images/1.3/Input5.png)
