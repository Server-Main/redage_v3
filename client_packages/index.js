const rpc = require("./rage-rpc.min.js");
global.rpc = rpc;
require("./main.js");
require('./modules/attachment/body.js');
require('./modules/attachment/sync.js');
require('./admin/adminpanel.js');