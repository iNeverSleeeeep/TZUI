local Commands = {
    {
        "lua",
        function(params) dostring(params[0].String, "GM") end,
        1, -1,
        "执行lua代码"
    },
}
return Commands