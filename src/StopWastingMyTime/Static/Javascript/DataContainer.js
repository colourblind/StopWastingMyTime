function DataContainer(data, pageSize)
{
    this._data = data;

    // Paging
    
    this._pageSize = pageSize;
    this.page = 0;
    this.pageDown = function()
    {
        this.page = Math.max(0, this.page - 1); 
        this._update(); 
        return false;
    }
    this.pageUp = function()
    {
        this.page = Math.min(this.maxPage(), this.page + 1); 
        this._update();
        return false;
    }
    this.setPage = function(p)
    {
        if (p >= 0 && p <= this.maxPage())
            this.page = p;
        this._update();
        return false;
    }
    
    this.maxPage = function() { return Math.floor((this._applyFilters().length - 1) / this._pageSize) };
    this._page = function(d) { return d.slice(this.page * this._pageSize, (this.page + 1) * this._pageSize); }
    
    // Sorting
    
    this._sortAscending = true;
    this._sortProperty = null;
    this.toggleSortDir = function()
    {
        this._sortAscending = !this._sortAscending;
        return this._update();
    }
    this.setSortProperty = function(property)
    {
        if (property != this._sortProperty)
        {
            this._sortProperty = property;
            this._sortAscending = true;
            return this._update();
        }
        else
        {
            return this.toggleSortDir();
        }
    }
    
    // Filtering
    
    this._filters = [];
    
    this.isFilter = function(property, value)
    {
        for (var i = 0; i < this._filters.length; i ++)
        {
            if (this._filters[i].property == property && this._filters[i].value == value)
                return true;
        }
        return false;
    }
    
    this.addFilter = function(property, value)
    {
        this._filters.push({ 'property': property, 'value': value.toString() });
        this.page = 0;
        return this._update();
    }
    this.removeFilter = function(property)
    {
        this._filters = this._filters.filter(function(e) { return e.property != property; });
        this.page = 0;
        return this._update();
    }
    
    this._applyFilters = function()
    {
        var filters = this._filters;
        return this._data.filter(function(e) {
            var result = true;
            for (var i = 0; i < filters.length; i ++)
            {
                var o = Colourblind.getDeepProperty(e, filters[i].property);
                if (Colourblind.isArray(o) && o.length > 0)
                {
                    var inArray = false;
                    for (var j = 0; j < o.length; j ++)
                        inArray = inArray || o[j].toString() == filters[i].value;
                    result = result && inArray;
                }
                else
                    result = result && o.toString() == filters[i].value;
            }
            return result;
        });
    }

    this.clear = function()
    {
        this.page = 0;
        this._filters = [];
        this._sortAscending = true;
        this._sortProperty = null;
        return this._update();
    }
    
    this.getPropertyValues = function(name)
    {
        var data = this._applyFilters();
        var result = [];
        for (var i = 0; i < data.length; i ++)
        {
            var o = Colourblind.getDeepProperty(data[i], name)
            if (o !== undefined)
            {
                if (Colourblind.isArray(o))
                {
                    for (var j = 0; j < o.length; j ++)
                    {
                        if (result.indexOf(o[j].toString()) == -1)
                            result.push(o[j].toString());
                    }
                }
                else
                {
                    if (result.indexOf(o.toString()) == -1)
                        result.push(o.toString());
                }
            }
        }
        return result.sort();
    }

    // Data fetching
    
    this.getData = function()
    {
        var data = this._applyFilters();
        if (this._sortProperty)
        {
            var sortProperty = this._sortProperty;
            var sortAscending = this._sortAscending;
            data.sort(function(a, b) {
                return (a[sortProperty] < b[sortProperty] ? -1 : 1) * (sortAscending ? 1 : -1);
            });
        }
        return this._page(data);
    }

    this.update = function() { }
    this._update = function()
    {
        this.update(); 
        return false;
    }
}

// Helper functions
var Colourblind = function()
{
    return {
        getDeepProperty : function(obj, name)
        {
            var propertyChain = name.split('.');
            var o = obj;
            for (var i = 0; i < propertyChain.length; i ++)
            {
                if ( o[propertyChain[i]] === undefined)
                    return undefined;
                o = o[propertyChain[i]];
            }
            return o;
        },
        isArray : function(obj)
        {
            return obj.constructor === Array;
        }
    };
}();

// Some ECMA-262 monkey patching for IE8 and below. Yoinked from Mozilla
if (!Array.prototype.indexOf) {  
    Array.prototype.indexOf = function (searchElement /*, fromIndex */ ) {  
        "use strict";  
        if (this === void 0 || this === null) {  
            throw new TypeError();  
        }  
        var t = Object(this);  
        var len = t.length >>> 0;  
        if (len === 0) {  
            return -1;  
        }  
        var n = 0;  
        if (arguments.length > 0) {  
            n = Number(arguments[1]);  
            if (n !== n) { // shortcut for verifying if it's NaN  
                n = 0;  
            } else if (n !== 0 && n !== Infinity && n !== -Infinity) {  
                n = (n > 0 || -1) * Math.floor(Math.abs(n));  
            }  
        }  
        if (n >= len) {  
            return -1;  
        }  
        var k = n >= 0 ? n : Math.max(len - Math.abs(n), 0);  
        for (; k < len; k++) {  
            if (k in t && t[k] === searchElement) {  
                return k;  
            }  
        }  
        return -1;  
    }  
}
if (!Array.prototype.filter)  
{  
    Array.prototype.filter = function(fun /*, thisp */)  
    {  
        "use strict";  
  
        if (this === void 0 || this === null)  
            throw new TypeError();  
  
        var t = Object(this);  
        var len = t.length >>> 0;  
        if (typeof fun !== "function")  
            throw new TypeError();  
  
        var res = [];  
        var thisp = arguments[1];  
        for (var i = 0; i < len; i++)  
        {  
            if (i in t)  
            {  
                var val = t[i]; // in case fun mutates this  
                if (fun.call(thisp, val, i, t))  
                    res.push(val);  
            }  
        }  
  
        return res;  
    };  
}
