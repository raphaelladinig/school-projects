local function load_languages(languages)
    for _, lang in ipairs(languages) do
        dofile(vim.fn.stdpath("config") .. "/lua/languages/" .. lang .. ".lua")
    end
end

load_languages({ "cpp" })

print("successfully loaded .nvim.lua")
