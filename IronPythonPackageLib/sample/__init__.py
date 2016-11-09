# -*- coding: utf-8 -*-

import locale
import gettext
import os

current_locale, encoding = locale.getdefaultlocale()
_ = gettext.translation('sample', 'locale', [current_locale], fallback = True).ugettext #unicode gettext